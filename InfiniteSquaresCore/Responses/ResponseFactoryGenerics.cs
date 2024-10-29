namespace InfiniteSquaresCore.Responses;

public static class ResponseFactoryGenerics<T>
{
    public static ResponseResult<T> Ok(T data, string? message = null) =>
        new(StatusCode.OK, data, message ?? "Succeeded");

    public static ResponseResult<T> Created(T data, string? message = null) =>
        new(StatusCode.CREATED, data, message ?? "Resource created successfully");

    public static ResponseResult<T> NoContent(string? message = null) =>
        new(StatusCode.NO_CONTENT, default, message ?? "No content available");

    public static ResponseResult<T> BadRequest(string? message = null) =>
        new(StatusCode.BAD_REQUEST, default, message ?? "Bad request");

    public static ResponseResult<T> NotFound(string? message = null) =>
        new(StatusCode.NOT_FOUND, default, message ?? "Resource not found");

    public static ResponseResult<T> Error(string? message = null) =>
        new(StatusCode.INTERNAL_SERVER_ERROR, default, message ?? "Internal server error");
}
