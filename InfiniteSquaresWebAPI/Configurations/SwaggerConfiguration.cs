using Microsoft.OpenApi.Models;

namespace InfiniteSquaresWebAPI.Configurations;

public static class SwaggerConfiguration
{
    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "InfiniteSquaresWebAPI", Version = "v1" });
        });
    }
}
