using InfiniteSquaresWebAPI.Middleware;

namespace InfiniteSquaresWebAPI.Configurations;

public static class MiddlewareConfiguration
{
    public static WebApplication UseCustomMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseRateLimiter();

        app.MapHealthChecks("/health");

        return app;
    }
}
