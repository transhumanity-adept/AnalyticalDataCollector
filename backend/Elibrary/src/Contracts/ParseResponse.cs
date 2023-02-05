namespace Elibrary.Contracts;

public class ParseResponse<T>
{
    public bool IsSuccess { get; set; }

    public string? ErrorMessage { get; set; }

    public T? Data { get; set; }

    public static ParseResponse<T> CreateOkResult(T data)
    {
        return new ParseResponse<T>()
        {
            IsSuccess = true,
            ErrorMessage = null,
            Data = data
        };
    }

    public static ParseResponse<T> CreateBadResult(string errorMessage)
    {
        return new ParseResponse<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Data = default
        };
    }
}