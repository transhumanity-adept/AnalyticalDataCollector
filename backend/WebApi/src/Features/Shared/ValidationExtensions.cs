using FluentValidation.Results;

namespace WebApi.Features.Shared;

public static class ValidationExtensions
{
    public static string ToPrettyString(this ValidationResult validationResult)
    {
        return string.Concat(' ', validationResult.Errors);
    }
}