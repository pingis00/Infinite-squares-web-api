namespace InfiniteSquaresCore.Responses;

public static class ResponseFactory
{
    public static ResponseResult Ok(string? message = null)
    {
        return new ResponseResult(StatusCode.OK, message ?? "Succeeded");
    }

    public static ResponseResult Created(string? message = null)
    {
        return new ResponseResult(StatusCode.CREATED, message ?? "Resource created successfully");
    }

    public static ResponseResult NoContent(string? message = null)
    {
        return new ResponseResult(StatusCode.NO_CONTENT, message ?? "No content available");
    }

    public static ResponseResult BadRequest(string? message = null)
    {
        return new ResponseResult(StatusCode.BAD_REQUEST, message ?? "Bad request");
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult(StatusCode.NOT_FOUND, message ?? "Resource not found");
    }


    public static ResponseResult InternalServerError(string? message = null)
    {
        return new ResponseResult(StatusCode.INTERNAL_SERVER_ERROR, message ?? "Internal server error");
    }
}
