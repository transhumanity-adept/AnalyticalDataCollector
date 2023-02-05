using FluentValidation;

namespace WebApi.Features.Tasks.Commands;

public class StopDataCollectionTaskCommandValidator : AbstractValidator<StopDataCollectionTaskCommand>
{
    public StopDataCollectionTaskCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .Must(x => x != Guid.Empty)
            .WithMessage("task id must be not empty");
    }
}