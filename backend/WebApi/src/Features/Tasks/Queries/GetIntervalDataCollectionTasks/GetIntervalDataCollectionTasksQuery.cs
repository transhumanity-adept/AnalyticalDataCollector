using MediatR;
using WebApi.Features.Tasks.Contracts;

namespace WebApi.Features.Tasks.Queries.GetIntervalDataCollectionTasks;

public class GetIntervalDataCollectionTasksQuery : IRequest<IntervalDataCollectionTaskDto> { }