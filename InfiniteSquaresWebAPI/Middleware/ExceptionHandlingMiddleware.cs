namespace InfiniteSquaresWebAPI.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var error = HandleException(exception);

            _logger.LogError(exception,
                "An error occurred: {Message}, RequestPath: {Path}",
                exception.Message,
                context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            await context.Response.WriteAsJsonAsync(error);
        }
    }

    private static ErrorResponse HandleException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => new ErrorResponse(
                StatusCodes.Status400BadRequest,
                "Required data was not provided."),

            ArgumentException => new ErrorResponse(
                StatusCodes.Status400BadRequest,
                "Invalid data was provided."),

            InvalidOperationException => new ErrorResponse(
                StatusCodes.Status400BadRequest,
                "The requested operation is invalid."),

            _ => new ErrorResponse(
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred. Please try again later.")
        };
    }
}

public record ErrorResponse(int StatusCode, string Message);

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
