namespace InfiniteSquaresCore.Responses;

public class ResponseResult<T>(StatusCode status, T? data = default, string? message = null)
{
    public StatusCode Status { get; set; } = status;
    public T? Data { get; set; } = data;
    public string? Message { get; set; } = message;
}

public class ResponseResult(StatusCode status, string? message)
{
    public StatusCode Status { get; set; } = status;
    public string? Message { get; set; } = message;
}
