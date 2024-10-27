namespace InfiniteSquaresCore.Responses;

public static class ResponseFactoryGenerics<T>
{
    public static ResponseResult<T> Ok(T data, string? message = null)
    {
        return new ResponseResult<T>(StatusCode.OK, data, message ?? "Succeeded");
    }

    public static ResponseResult<T> Created(T data, string? message = null)
    {
        return new ResponseResult<T>(StatusCode.CREATED, data, message ?? "Resource created successfully");
    }

    public static ResponseResult<T> Accepted(T data, string? message = null)
    {
        return new ResponseResult<T>(StatusCode.ACCEPTED, data, message ?? "Request accepted for processing");
    }

    public static ResponseResult<T> NoContent(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.NO_CONTENT, default, message ?? "No content available");
    }

    public static ResponseResult<T> BadRequest(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.BAD_REQUEST, default, message ?? "Bad request");
    }

    public static ResponseResult<T> Unauthorized(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.UNAUTHORIZED, default, message ?? "Unauthorized access");
    }

    public static ResponseResult<T> AccessDenied(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.ACCESS_DENIED, default, message ?? "Access denied");
    }

    public static ResponseResult<T> NotFound(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.NOT_FOUND, default, message ?? "Resource not found");
    }

    public static ResponseResult<T> Conflict(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.CONFLICT, default, message ?? "Conflict occurred");
    }

    public static ResponseResult<T> InternalServerError(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.INTERNAL_SERVER_ERROR, default, message ?? "Internal server error");
    }

    public static ResponseResult<T> NotImplemented(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.NOT_IMPLEMENTED, default, message ?? "Not implemented");
    }

    public static ResponseResult<T> BadGateway(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.BAD_GATEWAY, default, message ?? "Bad gateway");
    }

    public static ResponseResult<T> ServiceUnavailable(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.SERVICE_UNAVAILABLE, default, message ?? "Service unavailable");
    }

    public static ResponseResult<T> GatewayTimeout(string? message = null)
    {
        return new ResponseResult<T>(StatusCode.GATEWAY_TIMEOUT, default, message ?? "Gateway timeout");
    }
}
