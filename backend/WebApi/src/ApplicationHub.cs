using Microsoft.AspNetCore.SignalR;
using WebApi.Features.Tasks.Contracts;

namespace WebApi;

public class ApplicationHub : Hub
{
    public async Task SendUpdateTaskNotificationAsync(Guid taskId, int newCurrentArticleId, int competePercent)
    {
        await Clients.Client("frontend").SendAsync("updateTask", new
        {
            TaskId = taskId,
            NewCurrentArticleId = newCurrentArticleId,
            CompletePercent = competePercent
        });
    }

    public async Task SendLogTaskNotification(DataCollectionTaskLogDto logDto)
    {
        await Clients.Client("frontend").SendAsync("logTask", logDto);
    }
}