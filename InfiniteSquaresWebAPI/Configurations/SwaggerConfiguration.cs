using Microsoft.OpenApi.Models;

namespace InfiniteSquaresWebAPI.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Infinite Squares API",
                Version = "v1",
                Description = "API for managing infinite squares application"
            });
        });

        return services;
    }
}
