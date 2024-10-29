using InfiniteSquaresCore.Configurations;
using InfiniteSquaresCore.Interfaces.Repositories;
using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Models;
using InfiniteSquaresInfrastructure.Repositories;
using InfiniteSquaresInfrastructure.Services;

namespace InfiniteSquaresWebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var squareFilePath = configuration.GetValue<string>("FileSettings:SquareFilePath");

        if (string.IsNullOrWhiteSpace(squareFilePath))
        {
            throw new InvalidOperationException("Square file path is not configured properly.");
        }

        services.AddSingleton(new FileSettings { SquareFilePath = squareFilePath });

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IMappingService, MappingService>();
        services.AddScoped<ISquareService, SquareService>();
        services.AddScoped<IBaseRepository<Square>, BaseRepository<Square>>();
        services.AddScoped<ISquareRepository, SquareRepository>();
    }
}
