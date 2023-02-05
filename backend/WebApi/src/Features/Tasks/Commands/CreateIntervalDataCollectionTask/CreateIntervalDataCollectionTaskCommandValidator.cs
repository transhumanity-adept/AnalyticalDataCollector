using FluentValidation;
using WebApi.Features.Tasks.Commands;

namespace WebApi.Features.Tasks.Validations;

public class CreateIntervalDataCollectionTaskCommandValidator: AbstractValidator<CreateIntervalDataCollectionTaskCommand>
{
    public CreateIntervalDataCollectionTaskCommandValidator()
    {
        RuleFor(x => x.FromArticleId)
            .Must(x => x >= 0)
            .WithMessage("from article id must be non negative number");
        RuleFor(x => x.ToArticleId)
            .Must(x => x >= 0)
            .WithMessage("to article id must be non negative number");
        RuleFor(x => new { x.ToArticleId, x.FromArticleId })
            .Must(x => x.ToArticleId >= x.FromArticleId)
            .WithMessage("to article id must be greater than or equal to from article id");
    }
}