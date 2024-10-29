using InfiniteSquaresCore.Configurations;
using InfiniteSquaresCore.Interfaces.Repositories;
using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Models;
using InfiniteSquaresInfrastructure.Repositories;
using InfiniteSquaresInfrastructure.Services;
using InfiniteSquaresWebAPI.Interfaces;
using InfiniteSquaresWebAPI.Services;
using Microsoft.Extensions.Options;

namespace InfiniteSquaresWebAPI.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // File path configuration
        var squareFilePath = configuration.GetValue<string>("FileSettings:SquareFilePath")
            ?? throw new InvalidOperationException("Square file path is not configured.");

        services.AddSingleton(new FileSettings { SquareFilePath = squareFilePath });
        services.AddSingleton<IValidateOptions<FileSettings>, FileSettingsValidator>();

        // ASP.NET Core services
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // CORS configuration
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        // Repository registrations
        services.AddScoped<IBaseRepository<Square>, BaseRepository<Square>>()
        .AddScoped<ISquareRepository, SquareRepository>();

        // Service registrations
        services.AddScoped<IFileService, FileService>()
                .AddScoped<ILoggerService, LoggerService>()
                .AddScoped<ISquareService, SquareService>()
                .AddScoped<IMappingService, MappingService>();

        return services;
    }
}
