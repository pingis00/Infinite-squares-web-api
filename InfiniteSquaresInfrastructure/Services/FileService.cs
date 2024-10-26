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
            await File.WriteAllTextAsync(filePath, jsonData, Encoding.UTF8);
            _logger.LogInfo($"File saved successfully at {filePath}");
            return ResponseFactory.Ok("File saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to save file at {filePath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to save file: {ex.Message}");
        }
    }

    public async Task<ResponseResult> ReadFromFileAsync<T>(string filepath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                _logger.LogWarning("File path is null or empty.");
                return ResponseFactory.BadRequest("File path cannot be empty.");
            }

            if (!File.Exists(filepath))
            {
                _logger.LogWarning($"File not found at {filepath}");
                return ResponseFactory.NotFound($"File not found at {filepath}");
            }

            var jsonData = await File.ReadAllTextAsync(filepath, Encoding.UTF8);
            var data = JsonConvert.DeserializeObject<T>(jsonData);

            if (data == null)
            {
                _logger.LogWarning($"Failed to deserialize data from file at {filepath}");
                return ResponseFactory.InternalServerError("Failed to deserialize data.");
            }

            _logger.LogInfo($"File read successfully at {filepath}");
            return ResponseFactory.Ok(data, "File read successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read file from {filepath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to read file: {ex.Message}");
        }
    }

    public async Task<ResponseResult> DeleteFileAsync(string filepath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                _logger.LogWarning("File path is null or empty.");
                return ResponseFactory.BadRequest("File path cannot be empty.");
            }

            if (!File.Exists(filepath))
            {
                _logger.LogWarning($"File not found for deletion: {filepath}");
                return ResponseFactory.NotFound($"File not found at {filepath}");
            }

            File.Delete(filepath);
            _logger.LogInfo($"File deleted successfully at {filepath}");
            return ResponseFactory.Ok("File deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete file at {filepath}.", ex);
            return ResponseFactory.InternalServerError($"Failed to delete file: {ex.Message}");
        }
    }
}
