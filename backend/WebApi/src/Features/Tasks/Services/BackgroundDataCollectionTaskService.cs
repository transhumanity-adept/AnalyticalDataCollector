using System.Collections.Concurrent;
using AutoMapper;
using Newtonsoft.Json;
using Elibrary;
using WebApi.Features.Articles.Models;
using WebApi.Features.Shared;
using WebApi.Features.Tasks.Contracts;
using WebApi.Features.Tasks.Models;

namespace WebApi.Features.Tasks.Services;

public class BackgroundDataCollectionTaskService: IBackgroundDataCollectionTaskService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ApplicationHub hub;
    private readonly ConcurrentDictionary<Guid, CancellationTokenSource> store = new();

    public BackgroundDataCollectionTaskService
    (
        IServiceProvider serviceProvider,
        ApplicationHub hub
    )
    {
        this.serviceProvider = serviceProvider;
        this.hub = hub;
    }

    public async Task<RequestExecutionResult<Guid>> StartDataCollectionTaskAsync(Guid taskId, List<int> articleIds)
    {
        var taskAlreadyInStore = store.ContainsKey(taskId);
        if (taskAlreadyInStore) return RequestExecutionResult<Guid>.Failure("task already running");

        var tokenSource = new CancellationTokenSource();
        Task.Run(async () =>
        {
            if (tokenSource.Token.IsCancellationRequested) return;
            
            var outsideCurrentArticleId = articleIds.First();
            await using var scope = serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            
            try
            {
                var parser = new ElibraryParser();
                foreach (var currentArticleId in articleIds.TakeWhile(currentArticleId => !tokenSource.Token.IsCancellationRequested))
                {
                    outsideCurrentArticleId = currentArticleId;
                    var completePercent = (int)Math.Floor((articleIds.IndexOf(currentArticleId) + 1.0) / articleIds.Count * 100);
                    await hub.SendUpdateTaskNotificationAsync(taskId, currentArticleId, completePercent);
                    var parseResult = await parser.GetArticleByIdAsync(currentArticleId);
                    if (parseResult.IsSuccess == false)
                    {
                        var taskLogModel = new DataCollectionTaskLog()
                        {
                            LogType = DataCollectionTaskLogTypes.Error.ToString(),
                            Message = JsonConvert.SerializeObject(new
                            {
                                ArticleId = currentArticleId,
                                Error = parseResult.ErrorMessage
                            })
                        };
                
                        context.DataCollectionTaskLogs.Add(taskLogModel);
                        await context.SaveChangesAsync(tokenSource.Token);

                        var taskLogDto = mapper.Map<DataCollectionTaskLogDto>(taskLogModel);
                        await hub.SendLogTaskNotification(taskLogDto);

                        continue;
                    }

                    var newArticleModel = mapper.Map<Article>(parseResult.Data);
                    var newArticleAuthorModels = parseResult.Data.Authors.Select(x => new ArticleAuthor()
                    {
                        ArticleId = parseResult.Data.Id,
                        FIO = x
                    });
                    var newArticleKeyWordModels = parseResult.Data.KeyWords.Select(x => new ArticleKeyWord()
                    {
                        ArticleId = parseResult.Data.Id,
                        Name = x
                    });
                    var newArticleOrganizationModels = parseResult.Data.OrgNames.Select(x => new ArticleOrganization()
                    {
                        ArticleId = parseResult.Data.Id,
                        Name = x
                    });
                    context.Articles.Add(newArticleModel);
                    context.ArticleAuthors.AddRange(newArticleAuthorModels);
                    context.ArticleKeyWords.AddRange(newArticleKeyWordModels);
                    context.ArticleOrganizations.AddRange(newArticleOrganizationModels);
                    await context.SaveChangesAsync(tokenSource.Token);
                    
                    await Task.Delay(2000, tokenSource.Token);
                }
            }
            catch (Exception e)
            {
                store.Remove(taskId, out _);
                var taskLogModel = new DataCollectionTaskLog()
                {
                    LogType = DataCollectionTaskLogTypes.Error.ToString(),
                    Message = JsonConvert.SerializeObject(new
                    {
                        ArticleId = outsideCurrentArticleId,
                        Error = e.Message
                    })
                };
                
                context.DataCollectionTaskLogs.Add(taskLogModel);
                await context.SaveChangesAsync(tokenSource.Token);

                var taskLogDto = mapper.Map<DataCollectionTaskLogDto>(taskLogModel);
                await hub.SendLogTaskNotification(taskLogDto);
            }
        }, tokenSource.Token);
        
        var addResult = store.TryAdd(taskId, tokenSource);
        
        if (addResult == false)
        {
            tokenSource.Cancel();
            return RequestExecutionResult<Guid>.Failure("task already running");
        }
        return RequestExecutionResult<Guid>.Success(taskId);
    }

    public async Task<RequestExecutionResult<Guid>> StopDataCollectionTaskAsync(Guid taskId)
    {
        var removeResult = store.TryRemove(taskId, out CancellationTokenSource tokenSource);
        if (removeResult == false) RequestExecutionResult<Guid>.Failure("task not found");
        tokenSource.Cancel();
        return RequestExecutionResult<Guid>.Success(taskId);
    }
}