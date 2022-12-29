namespace WebApi.Services;

public interface IDataCollectionTaskManager
{
    bool AddTask(Guid taskId, Task task, CancellationTokenSource cancellationToken);
    bool DropTask(Guid taskId);
}