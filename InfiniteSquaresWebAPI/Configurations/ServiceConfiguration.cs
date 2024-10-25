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
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IMappingService, MappingService>();
        services.AddScoped<ISquareService, SquareService>();
        services.AddScoped<IBaseRepository<Square>, BaseRepository<Square>>();
        services.AddScoped<ISquareRepository, SquareRepository>();
    }
}
