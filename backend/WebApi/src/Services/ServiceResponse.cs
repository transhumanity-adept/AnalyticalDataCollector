namespace WebApi.Services;

public class ServiceResponse<T>
{
    public bool IsSuccess { get; set; }

    public string? ErrorMessage { get; set; }

    public T? Data { get; set; }

    public static ServiceResponse<T> CreateOkResult(T data)
    {
        return new ServiceResponse<T>()
        {
            IsSuccess = true,
            ErrorMessage = null,
            Data = data
        };
    }

    public static ServiceResponse<T> CreateBadResult(string errorMessage)
    {
        return new ServiceResponse<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Data = default
        };
    }
}