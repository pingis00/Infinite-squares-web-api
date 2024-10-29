namespace InfiniteSquaresCore.Responses;

public static class ResponseFactory
{
    public static ResponseResult Ok(string? message = null) =>
        new(StatusCode.OK, message ?? "Succeeded");

    public static ResponseResult Created(string? message = null) =>
        new(StatusCode.CREATED, message ?? "Resource created successfully");

    public static ResponseResult NoContent(string? message = null) =>
        new(StatusCode.NO_CONTENT, message ?? "No content available");

    public static ResponseResult BadRequest(string? message = null) =>
        new(StatusCode.BAD_REQUEST, message ?? "Bad request");

    public static ResponseResult NotFound(string? message = null) =>
        new(StatusCode.NOT_FOUND, message ?? "Resource not found");

    public static ResponseResult Error(string? message = null) =>
        new(StatusCode.INTERNAL_SERVER_ERROR, message ?? "Internal server error");
}
