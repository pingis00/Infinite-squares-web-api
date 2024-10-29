using InfiniteSquaresCore.Configurations;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace InfiniteSquaresWebAPI.Configurations;

public class FileSettingsValidator(ILogger<FileSettingsValidator> logger) : IValidateOptions<FileSettings>
{
    private readonly ILogger<FileSettingsValidator> _logger = logger;

    public ValidateOptionsResult Validate(string? name, FileSettings options)
    {
        if (string.IsNullOrWhiteSpace(options.SquareFilePath))
        {
            _logger.LogError("SquareFilePath must be configured in settings");
            return ValidateOptionsResult.Fail("SquareFilePath must be configured in settings.");
        }

        try
        {
            var directory = Path.GetDirectoryName(options.SquareFilePath);
            if (string.IsNullOrWhiteSpace(directory))
            {
                _logger.LogError("Invalid file path specified");
                return ValidateOptionsResult.Fail("Invalid file path specified.");
            }

            if (!Directory.Exists(directory))
            {
                try
                {
                    _logger.LogInformation("Creating directory: {Directory}", directory);
                    Directory.CreateDirectory(directory);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create directory: {Directory}", directory);
                    return ValidateOptionsResult.Fail($"Failed to create directory: {ex.Message}");
                }
            }
            _logger.LogInformation("File settings validation successful");
            return ValidateOptionsResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Invalid file path: {FilePath}", options.SquareFilePath);
            return ValidateOptionsResult.Fail($"Invalid file path: {ex.Message}");
        }

    }
}
