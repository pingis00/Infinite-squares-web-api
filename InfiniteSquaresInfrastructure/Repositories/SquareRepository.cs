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
            ArgumentNullException.ThrowIfNull(entity);

            var existingSquaresResult = await _fileService.ReadFromFileAsync<List<Square>>(_filePath);

            if (!IsSuccessOrNotFound(existingSquaresResult.Status))
            {
                _logger.LogError("Failed to read existing squares from file at {FilePath}", _filePath);
                return ResponseFactory.Error("Failed to read existing squares.");
            }

            var squares = existingSquaresResult.Data ?? [];

            squares.Add(entity);

            var saveResult = await _fileService.SaveFileAsync(_filePath, squares);

            return saveResult.Status == StatusCode.CREATED
                ? ResponseFactory.Created("Square created successfully.")
                : ResponseFactory.Ok("Square added successfully.");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating square at {filePath}", _filePath);
            return ResponseFactory.Error("Failed to create square.");
        }
    }

    public async Task<ResponseResult<IEnumerable<Square>>> GetAllAsync()
    {
        try
        {
            var result = await _fileService.ReadFromFileAsync<List<Square>>(_filePath);

            return result.Status switch
            {
                StatusCode.NOT_FOUND => ResponseFactoryGenerics<IEnumerable<Square>>.NotFound("No squares found."),
                StatusCode.OK => ResponseFactoryGenerics<IEnumerable<Square>>.Ok(result.Data ?? []),
                _ => ResponseFactoryGenerics<IEnumerable<Square>>.Error("Failed to retrieve squares.")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving squares from file at {filePath}", _filePath);
            return ResponseFactoryGenerics<IEnumerable<Square>>.Error("Failed to retrieve squares.");
        }
    }

    public async Task<ResponseResult> DeleteAsync()
    {
        try
        {
            var deleteResult = await _fileService.DeleteFileAsync(_filePath);

            return deleteResult.Status == StatusCode.OK
                ? ResponseFactory.Ok("All squares deleted successfully.")
                : ResponseFactory.Error("Failed to delete squares.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting squares from file at {filePath}", _filePath);
            return ResponseFactory.Error("Failed to delete square.");
        }
    }

    private static bool IsSuccessOrNotFound(StatusCode status) =>
    status is StatusCode.OK or StatusCode.NOT_FOUND;
}
