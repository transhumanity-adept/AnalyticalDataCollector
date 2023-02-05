using MediatR;
using WebApi.Features.Tasks.Contracts;

namespace WebApi.Features.Tasks.Queries.GetIntervalDataCollectionTasks;

public class GetIntervalDataCollectionTasksHandler : IRequestHandler<GetIntervalDataCollectionTasksQuery, IntervalDataCollectionTaskDto>
{
    private readonly DatabaseContext context;

    public GetIntervalDataCollectionTasksHandler
    (
        DatabaseContext context
    )
    {
        this.context = context;
    }

    public Task<IntervalDataCollectionTaskDto> Handle(GetIntervalDataCollectionTasksQuery request, CancellationToken cancellationToken)
    {
        
    }
}