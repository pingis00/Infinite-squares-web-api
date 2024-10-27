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

    public static ResponseResult Accepted(string? message = null)
    {
        return new ResponseResult(StatusCode.ACCEPTED, message ?? "Request accepted for processing");
    }

    public static ResponseResult NoContent(string? message = null)
    {
        return new ResponseResult(StatusCode.NO_CONTENT, message ?? "No content available");
    }

    public static ResponseResult BadRequest(string? message = null)
    {
        return new ResponseResult(StatusCode.BAD_REQUEST, message ?? "Bad request");
    }

    public static ResponseResult Unauthorized(string? message = null)
    {
        return new ResponseResult(StatusCode.UNAUTHORIZED, message ?? "Unauthorized access");
    }

    public static ResponseResult AccessDenied(string? message = null)
    {
        return new ResponseResult(StatusCode.ACCESS_DENIED, message ?? "Access denied");
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult(StatusCode.NOT_FOUND, message ?? "Resource not found");
    }

    public static ResponseResult Conflict(string? message = null)
    {
        return new ResponseResult(StatusCode.CONFLICT, message ?? "Conflict occurred");
    }

    public static ResponseResult InternalServerError(string? message = null)
    {
        return new ResponseResult(StatusCode.INTERNAL_SERVER_ERROR, message ?? "Internal server error");
    }

    public static ResponseResult NotImplemented(string? message = null)
    {
        return new ResponseResult(StatusCode.NOT_IMPLEMENTED, message ?? "Not implemented");
    }

    public static ResponseResult BadGateway(string? message = null)
    {
        return new ResponseResult(StatusCode.BAD_GATEWAY, message ?? "Bad gateway");
    }

    public static ResponseResult ServiceUnavailable(string? message = null)
    {
        return new ResponseResult(StatusCode.SERVICE_UNAVAILABLE, message ?? "Service unavailable");
    }

    public static ResponseResult GatewayTimeout(string? message = null)
    {
        return new ResponseResult(StatusCode.GATEWAY_TIMEOUT, message ?? "Gateway timeout");
    }
}
