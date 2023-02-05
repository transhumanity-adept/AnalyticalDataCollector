using WebApi.Features.Shared;

namespace WebApi.Features.Tasks.Services;

public interface IBackgroundDataCollectionTaskService
{
    Task<RequestExecutionResult<Guid>> StartDataCollectionTaskAsync(Guid taskId, List<int> articleIds);
    Task<RequestExecutionResult<Guid>> StopDataCollectionTaskAsync(Guid taskId);
}