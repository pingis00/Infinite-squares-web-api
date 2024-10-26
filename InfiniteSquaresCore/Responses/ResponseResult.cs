namespace InfiniteSquaresCore.Responses;

public class ResponseResult
{
    public StatusCode Status { get; set; }
    public object? ContentResult { get; set; }
    public string? Message { get; set; }
}
