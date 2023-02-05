namespace WebApi.Features.Shared;

public class RequestExecutionResult<TResult>
{
    private RequestExecutionResult(bool isSuccess, TResult? result, string? errorMessage)
    {
        this.IsSuccess = isSuccess;
        this.Result = result;
        this.ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }

    public TResult? Result { get; }

    public string? ErrorMessage { get; }

    public static RequestExecutionResult<TResult> Failure(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new ArgumentException("Error message must be not null or only white space", nameof(errorMessage));
        }
        
        return new RequestExecutionResult<TResult>
        (
            isSuccess: false,
            result: default,
            errorMessage: errorMessage
        );
    }

    public static RequestExecutionResult<TResult> Success(TResult result)
    {
        return new RequestExecutionResult<TResult>
        (
            isSuccess: true,
            result: result,
            errorMessage: null
        );
    }
}