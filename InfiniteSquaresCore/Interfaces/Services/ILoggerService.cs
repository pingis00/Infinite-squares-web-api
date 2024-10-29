namespace InfiniteSquaresCore.Interfaces.Services;

public interface ILoggerService
{
    void LogError(string message, Exception? ex = null);
    void LogInfo(string message);
    void LogWarning(string message);
}
