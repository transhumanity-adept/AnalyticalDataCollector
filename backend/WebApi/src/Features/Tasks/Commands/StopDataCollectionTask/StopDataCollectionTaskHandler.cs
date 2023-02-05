using MediatR;
using WebApi.Features.Shared;
using WebApi.Features.Tasks.Services;

namespace WebApi.Features.Tasks.Commands;

public class StopDataCollectionTaskHandler : IRequestHandler<StopDataCollectionTaskCommand, RequestExecutionResult<Guid>>
{
    private readonly IBackgroundDataCollectionTaskService backgroundDataCollectionTaskService;

    public StopDataCollectionTaskHandler
    (
        IBackgroundDataCollectionTaskService backgroundDataCollectionTaskService
    )
    {
        this.backgroundDataCollectionTaskService = backgroundDataCollectionTaskService;
    }

    public async Task<RequestExecutionResult<Guid>> Handle(StopDataCollectionTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stopResult = await backgroundDataCollectionTaskService.StopDataCollectionTaskAsync(request.TaskId);
            return stopResult.IsSuccess
                ? RequestExecutionResult<Guid>.Success(stopResult.Result)
                : RequestExecutionResult<Guid>.Failure(stopResult.ErrorMessage);
        }
        catch (Exception e)
        {
            return RequestExecutionResult<Guid>.Failure(e.Message);
        }
    }
}