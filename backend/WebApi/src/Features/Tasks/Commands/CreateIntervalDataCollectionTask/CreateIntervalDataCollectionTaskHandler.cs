using AutoMapper;
using FluentValidation;
using MediatR;
using WebApi.Features.Shared;
using WebApi.Features.Tasks.Contracts;
using WebApi.Features.Tasks.Models;
using WebApi.Features.Tasks.Services;

namespace WebApi.Features.Tasks.Commands;

public class CreateIntervalDataCollectionTaskHandler : IRequestHandler<CreateIntervalDataCollectionTaskCommand, RequestExecutionResult<IntervalDataCollectionTaskDto>>
{
    private readonly IValidator<CreateIntervalDataCollectionTaskCommand> validator;
    private readonly IMapper mapper;
    private readonly DatabaseContext context;
    private readonly IBackgroundDataCollectionTaskService backgroundDataCollectionTaskService;

    public CreateIntervalDataCollectionTaskHandler
    (
        IValidator<CreateIntervalDataCollectionTaskCommand> validator,
        IMapper mapper,
        DatabaseContext context,
        IBackgroundDataCollectionTaskService backgroundDataCollectionTaskService
    )
    {
        this.validator = validator;
        this.mapper = mapper;
        this.context = context;
        this.backgroundDataCollectionTaskService = backgroundDataCollectionTaskService;
    }
    
    public async Task<RequestExecutionResult<IntervalDataCollectionTaskDto>> Handle(CreateIntervalDataCollectionTaskCommand request, CancellationToken cancellationToken)
    {
        var validateResponse = await validator.ValidateAsync(request, cancellationToken);
        if (validateResponse.IsValid == false)
        {
            return RequestExecutionResult<IntervalDataCollectionTaskDto>.Failure(validateResponse.ToPrettyString());
        }

        var taskId = Guid.NewGuid();
        var newTaskGeneralModel = mapper.Map<DataCollectionTaskGeneral>(request);
        newTaskGeneralModel.Id = taskId;
        context.DataCollectionTasks.Add(newTaskGeneralModel);
        
        var intervalTaskMode = mapper.Map<IntervalDataCollectionTask>(request);
        intervalTaskMode.Id = taskId;
        context.IntervalDataCollectionTasks.Add(intervalTaskMode);
        
        await context.SaveChangesAsync(cancellationToken);

        var ids = Enumerable.Range(request.FromArticleId, request.ToArticleId - request.FromArticleId + 1).ToList();
        var startResult = await backgroundDataCollectionTaskService.StartDataCollectionTaskAsync(newTaskModel.Id, ids);
        if (startResult.IsSuccess == false)
        {
            return RequestExecutionResult<IntervalDataCollectionTaskDto>.Failure(startResult.ErrorMessage);
        }

        var newTaskDto = mapper.Map<DataCollectionTaskGeneralDto>(newTaskModel);
        
        return RequestExecutionResult<IntervalDataCollectionTaskDto>.Success(newTaskDto);
    }
}