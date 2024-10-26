namespace InfiniteSquaresCore.Responses;

public static class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message = "Succeeded",
            Status = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object content, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = content,
            Message = message ?? "Succeeded",
            Status = StatusCode.OK,
        };
    }

    public static ResponseResult Created(object content, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = content,
            Message = message ?? "Resource created successfully",
            Status = StatusCode.CREATED
        };
    }

    public static ResponseResult Accepted(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Request accepted for processing",
            Status = StatusCode.ACCEPTED
        };
    }

    public static ResponseResult NoContent(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "No content available",
            Status = StatusCode.NO_CONTENT
        };
    }

    public static ResponseResult BadRequest(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Bad request",
            Status = StatusCode.BAD_REQUEST
        };
    }

    public static ResponseResult Unauthorized(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Unauthorized access",
            Status = StatusCode.UNAUTHORIZED
        };
    }

    public static ResponseResult AccessDenied(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Access denied",
            Status = StatusCode.ACCESS_DENIED
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Resource not found",
            Status = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult MethodNotAllowed(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Method not allowed",
            Status = StatusCode.METHOD_NOT_ALLOWED
        };
    }

    public static ResponseResult Conflict(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Conflict occurred",
            Status = StatusCode.CONFLICT
        };
    }

    public static ResponseResult InternalServerError(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Internal server error",
            Status = StatusCode.INTERNAL_SERVER_ERROR
        };
    }

    public static ResponseResult NotImplemented(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not implemented",
            Status = StatusCode.NOT_IMPLEMENTED
        };
    }

    public static ResponseResult BadGateway(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Bad gateway",
            Status = StatusCode.BAD_GATEWAY
        };
    }

    public static ResponseResult ServiceUnavailable(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Service unavailable",
            Status = StatusCode.SERVICE_UNAVAILABLE
        };
    }

    public static ResponseResult GatewayTimeout(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Gateway timeout",
            Status = StatusCode.GATEWAY_TIMEOUT
        };
    }
}
