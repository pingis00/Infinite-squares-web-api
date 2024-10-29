using InfiniteSquaresCore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InfiniteSquaresInfrastructure.Services;

public class LoggerService(ILogger<LoggerService> logger) : ILoggerService
{
    private readonly ILogger<LoggerService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public void LogError(string message, Exception? ex = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        _logger.LogError(ex, "Error: {Message}", message);
    }

    public void LogInfo(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        _logger.LogInformation("Info: {Message}", message);
    }

    public void LogWarning(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        _logger.LogWarning("Warning: {Message}", message);
    }
}
