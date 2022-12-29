using WebApi.Models;

namespace WebApi.Services;

public interface IDataCollectionTaskService
{
    Task<ServiceResponse<CreateDataCollectionTaskResponse>> CreateDataCollectionTaskAsync(CreateDataCollectionTaskRequest request);
}