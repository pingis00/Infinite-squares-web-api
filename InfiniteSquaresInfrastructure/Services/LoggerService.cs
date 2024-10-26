using InfiniteSquaresCore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InfiniteSquaresInfrastructure.Services;

public class LoggerService(ILogger<LoggerService> logger) : ILoggerService
{
    private readonly ILogger<LoggerService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public void LogError(string message, Exception ex = null!)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Log message cannot be empty.", nameof(message));
        }

        if (ex == null)
        {
            _logger.LogError("Error: {Message}", message);
        }
        else
        {
            _logger.LogError(ex, "Error: {Message}", message);
        }
    }

    public void LogInfo(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Log message cannot be empty.", nameof(message));
        }

        _logger.LogInformation("Info: {Message}", message);
    }

    public void LogWarning(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Log message cannot be empty.", nameof(message));
        }

        _logger.LogWarning("Warning: {Message}", message);
    }
}
