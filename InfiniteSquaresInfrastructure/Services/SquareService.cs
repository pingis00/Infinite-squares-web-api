using InfiniteSquaresCore.Interfaces.Repositories;
using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Models;
using InfiniteSquaresCore.Responses;
using Microsoft.Extensions.Logging;

namespace InfiniteSquaresInfrastructure.Services;

public class SquareService(ISquareRepository squareRepository, ILogger<SquareService> logger) : ISquareService
{
    private readonly ISquareRepository _squareRepository = squareRepository;
    private readonly ILogger<SquareService> _logger = logger;

    public async Task<ResponseResult> CreateSquareAsync(Square square)
    {
        try
        {
            var result = await _squareRepository.CreateAsync(square);
            if (result.Status != StatusCode.CREATED && result.Status != StatusCode.OK)
            {
                _logger.LogError("Failed to create square with ID {SquareId}.", square.Id);
                return ResponseFactory.InternalServerError("Failed to create square.");
            }
            _logger.LogInformation("Square with ID {SquareId} created successfully.", square.Id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating square with ID {SquareId}.", square.Id);
            return ResponseFactory.InternalServerError("Error creating square.");
        }
    }

    public async Task<ResponseResult<IEnumerable<Square>>> GetAllSquaresAsync()
    {
        try
        {
            var result = await _squareRepository.GetAllAsync();
            if (result.Status != StatusCode.OK)
            {
                _logger.LogError("Failed to retrieve squares.");
                return ResponseFactoryGenerics<IEnumerable<Square>>.Error("Failed to retrieve squares.");
            }
            _logger.LogInformation("Retrieved all squares successfully.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving squares.");
            return ResponseFactoryGenerics<IEnumerable<Square>>.Error("Error retrieving squares.");
        }
    }

    public async Task<ResponseResult> DeleteAllSquaresAsync()
    {
        try
        {
            var result = await _squareRepository.DeleteAsync();
            if (result.Status != StatusCode.OK)
            {
                _logger.LogError("Failed to delete all squares.");
                return ResponseFactory.Error("Failed to delete squares.");
            }
            _logger.LogInformation("All squares deleted successfully.");
            return ResponseFactory.Ok("All squares deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting all squares.");
            return ResponseFactory.Error("Error deleting squares.");
        }
    }
}
