using InfiniteSquaresCore.Configurations;
using InfiniteSquaresCore.Interfaces.Repositories;
using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Models;
using InfiniteSquaresCore.Responses;
using Microsoft.Extensions.Logging;

namespace InfiniteSquaresInfrastructure.Repositories;

public class SquareRepository(IFileService fileService, ILogger<SquareRepository> logger, FileSettings fileSettings) : ISquareRepository
{
    private readonly IFileService _fileService = fileService;
    private readonly ILogger<SquareRepository> _logger = logger;
    private readonly string _filePath = fileSettings.SquareFilePath;

    public async Task<ResponseResult> CreateAsync(Square entity)
    {
        try
        {
            if (entity == null)
            {
                _logger.LogError("Failed to read existing squares from file at {FilePath}.", _filePath);
                return ResponseFactory.BadRequest("Square entity cannot be null.");
            }

            var existingSquaresResult = await _fileService.ReadFromFileAsync<List<Square>>(_filePath);

            if (existingSquaresResult.Status != StatusCode.OK && existingSquaresResult.Status != StatusCode.NOT_FOUND)
            {
                _logger.LogError("Failed to read existing squares from file at {filePath}", _filePath);
                return ResponseFactory.InternalServerError("Failed to read existing squares.");
            }

            var squares = existingSquaresResult.Data ?? [];

            squares.Add(entity);

            var saveResult = await _fileService.SaveFileAsync(_filePath, squares);

            if (saveResult.Status == StatusCode.CREATED)
            {
                _logger.LogInformation("Square created and file saved successfully at {filePath}", _filePath);
                return ResponseFactory.Created("Square created and file saved successfully.");
            }
            else
            {
                _logger.LogInformation("Square added to existing file successfully at {filePath}", _filePath);
                return ResponseFactory.Ok("Square added to existing file successfully.");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating square at {filePath}", _filePath);
            return ResponseFactory.InternalServerError("Failed to create square.");
        }
    }

    public async Task<ResponseResult<IEnumerable<Square>>> GetAllAsync()
    {
        try
        {
            var result = await _fileService.ReadFromFileAsync<List<Square>>(_filePath);

            if (result.Status == StatusCode.NOT_FOUND)
            {
                _logger.LogWarning("No squares found at {filePath}", _filePath);
                return ResponseFactoryGenerics<IEnumerable<Square>>.NotFound("No squares found.");
            }

            if (result.Status != StatusCode.OK)
            {
                _logger.LogError("Failed to retrieve squares from file at {filePath}", _filePath);
                return ResponseFactoryGenerics<IEnumerable<Square>>.InternalServerError("Failed to retrieve squares.");
            }

            var squares = result.Data ?? [];
            return ResponseFactoryGenerics<IEnumerable<Square>>.Ok(squares);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving squares from file at {filePath}", _filePath);
            return ResponseFactoryGenerics<IEnumerable<Square>>.InternalServerError("Failed to retrieve squares.");
        }
    }

    public async Task<ResponseResult> DeleteAsync()
    {
        try
        {
            var deleteResult = await _fileService.DeleteFileAsync(_filePath);

            if (deleteResult.Status != StatusCode.OK)
            {
                _logger.LogError("Failed to delete squares from file at {filePath}", _filePath);
                return ResponseFactory.InternalServerError("Failed to delete squares.");
            }

            _logger.LogInformation("All squares deleted successfully from file at {filePath}", _filePath);
            return ResponseFactory.Ok("All squares deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting squares from file at {filePath}", _filePath);
            return ResponseFactory.InternalServerError("Failed to delete square.");
        }
    }
}
