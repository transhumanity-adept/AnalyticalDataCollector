using WebApi.Models;

namespace WebApi.Services;

public class DataCollectionTaskService : IDataCollectionTaskService
{
    private readonly IDataCollectionTaskManager dataCollectionTaskManager;
    private readonly DatabaseContext databaseContext;

    public DataCollectionTaskService
    (
        IDataCollectionTaskManager dataCollectionTaskManager,
        DatabaseContext databaseContext
    )
    {
        this.dataCollectionTaskManager = dataCollectionTaskManager;
        this.databaseContext = databaseContext;
    }

    public async Task<ServiceResponse<CreateDataCollectionTaskResponse>> CreateDataCollectionTaskAsync(CreateDataCollectionTaskRequest request)
    {
        try
        {
            DataCollectionTask newTask = new()
            {
                Id = Guid.NewGuid(),
                FilePath = "test",
                StartId = request.StartId,
                EndId = request.EndId
            };
            databaseContext.Add(newTask);
            await databaseContext.SaveChangesAsync();
            CreateDataCollectionTaskResponse response = new();
            return ServiceResponse<CreateDataCollectionTaskResponse>.CreateOkResult(response);
        } catch (Exception e)
        {
            return ServiceResponse<CreateDataCollectionTaskResponse>.CreateBadResult(e.Message);
        }
        // Создание задачи парсинга диапазона объектов по id
        // Сохранение задачи в локальное хранилище
        // Добавление информации о задаче в бд
        var cancellationTokenSource = new CancellationTokenSource();
        throw new NotImplementedException();
    }
}