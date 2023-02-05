using MediatR;
using WebApi.Features.Shared;

namespace WebApi.Features.Tasks.Commands;

public class StopDataCollectionTaskCommand : IRequest<RequestExecutionResult<Guid>>
{
    public Guid TaskId { get; set; }
}