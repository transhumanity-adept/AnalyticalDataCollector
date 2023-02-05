using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Tasks.Commands;
using WebApi.Features.Tasks.Contracts;

namespace WebApi.Features.Tasks.Controllers;

[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;
    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<DataCollectionTaskGeneralDto>> CreateDataCollectionTaskAsync([FromBody] CreateIntervalDataCollectionTaskCommand createIntervalCommand)
    {
        var createResult = await mediator.Send(createIntervalCommand);
        return createResult.IsSuccess ? Ok(createResult.Result) : BadRequest(createResult.ErrorMessage);
    }
}