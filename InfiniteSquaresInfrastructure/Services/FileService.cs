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
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(data);

        try
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            var fileExists = File.Exists(filePath);

            var directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllTextAsync(filePath, jsonData, Encoding.UTF8);

            _logger.LogInfo($"File {(fileExists ? "updated" : "created")} successfully at {filePath}");

            return fileExists
                ? ResponseFactory.Ok("File updated successfully.")
                : ResponseFactory.Created("File created successfully.");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to save file at {filePath}", ex);
            return ResponseFactory.Error("Failed to save file.");
        }
    }

    public async Task<ResponseResult<T>> ReadFromFileAsync<T>(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found at {filePath}");
                return ResponseFactoryGenerics<T>.NotFound("File not found");
            }

            var jsonData = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            var data = JsonConvert.DeserializeObject<T>(jsonData);
            if (data is null)
            {
                _logger.LogWarning($"Failed to deserialize data from file at {filePath}");
                return ResponseFactoryGenerics<T>.Error("Failed to deserialize data");
            }

            _logger.LogInfo($"File read successfully at {filePath}");
            return ResponseFactoryGenerics<T>.Ok(data, "File read successfully");
        }
        catch (JsonException ex)
        {
            _logger.LogError($"Failed to deserialize JSON from {filePath}", ex);
            return ResponseFactoryGenerics<T>.Error("Invalid JSON format");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read file from {filePath}", ex);
            return ResponseFactoryGenerics<T>.Error($"Failed to read file: {ex.Message}");
        }
    }

    public async Task<ResponseResult> DeleteFileAsync(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found at {filePath}");
                return ResponseFactory.NotFound("File not found.");
            }

            await Task.Run(() => File.Delete(filePath));
            _logger.LogInfo($"File deleted successfully at {filePath}");
            return ResponseFactory.Ok("File deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete file at {filePath}", ex);
            return ResponseFactory.Error("Failed to delete file.");
        }
    }
}
