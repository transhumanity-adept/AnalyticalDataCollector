using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/data-collection-tasks")]
public class TaskController : ControllerBase
{
    private readonly IDataCollectionTaskService dataCollectionTaskService;
    public TaskController(IDataCollectionTaskService dataCollectionTaskService)
    {
        this.dataCollectionTaskService = dataCollectionTaskService;
    }

    [HttpPost]
    public async Task<ActionResult<CreateDataCollectionTaskResponse>> CreateDataCollectionTaskAsync([FromBody] CreateDataCollectionTaskRequest request)
    {
        var createResult = await dataCollectionTaskService.CreateDataCollectionTaskAsync(request);
        return createResult.IsSuccess ? Ok(createResult.Data) : BadRequest(createResult.ErrorMessage);
    }
}