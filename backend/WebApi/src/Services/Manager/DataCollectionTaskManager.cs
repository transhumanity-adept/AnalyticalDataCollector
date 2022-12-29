using System.Collections.Concurrent;

namespace WebApi.Services;

public class DataCollectionTaskManager : IDataCollectionTaskManager
{
    private readonly ConcurrentDictionary<Guid, (Task task, CancellationTokenSource cancellationToken)> store = new();
    public bool AddTask(Guid taskId, Task task, CancellationTokenSource cancellationToken)
    {
        return store.TryAdd(taskId, (task, cancellationToken));
    }

    public bool DropTask(Guid taskId)
    {
        bool getSuccess = store.TryRemove(taskId, out (Task task, CancellationTokenSource cancellationToken) taskInfo);
        if (getSuccess == false) return false;
        taskInfo.cancellationToken.Cancel();
        return true;
    }
}