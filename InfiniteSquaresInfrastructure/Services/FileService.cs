using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Responses;
using Newtonsoft.Json;
using System.Text;

namespace InfiniteSquaresInfrastructure.Services;

public class FileService(ILoggerService logger) : IFileService
{
    private readonly ILoggerService _logger = logger;

    public async Task<ResponseResult> SaveFileAsync<T>(string filePath, T data)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogWarning("File path is null or empty.");
                return ResponseFactory.BadRequest("File path cannot be empty.");
            }

            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            var fileExists = File.Exists(filePath);
            await File.WriteAllTextAsync(filePath, jsonData, Encoding.UTF8);

            var message = fileExists ? "File updated successfully." : "File saved successfully.";
            _logger.LogInfo($"{message} at {filePath}");
            return ResponseFactory.Ok(message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to save file at {filePath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to save file: {ex.Message}");
        }
    }

    public async Task<ResponseResult> ReadFromFileAsync<T>(string filePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogWarning("File path is null or empty.");
                return ResponseFactory.BadRequest("File path cannot be empty.");
            }

            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found at {filePath}");
                return ResponseFactory.NotFound($"File not found at {filePath}");
            }

            var jsonData = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            var data = JsonConvert.DeserializeObject<T>(jsonData);

            if (data == null)
            {
                _logger.LogWarning($"Failed to deserialize data from file at {filePath}");
                return ResponseFactory.InternalServerError("Failed to deserialize data.");
            }

            _logger.LogInfo($"File read successfully at {filePath}");
            return ResponseFactory.Ok(data, "File read successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read file from {filePath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to read file: {ex.Message}");
        }
    }

    public async Task<ResponseResult> DeleteFileAsync(string filePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogWarning("File path is null or empty.");
                return ResponseFactory.BadRequest("File path cannot be empty.");
            }

            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found for deletion: {filePath}");
                return ResponseFactory.NotFound($"File not found at {filePath}");
            }

            File.Delete(filePath);
            _logger.LogInfo($"File deleted successfully at {filePath}");
            return ResponseFactory.Ok("File deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete file at {filePath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to delete file: {ex.Message}");
        }
    }
}
