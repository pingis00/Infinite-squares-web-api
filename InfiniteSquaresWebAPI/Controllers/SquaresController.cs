using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresWebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using static InfiniteSquaresCore.Responses.StatusCode;

namespace InfiniteSquaresWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController(ISquareService squaresService, IMappingService mappingService, ILogger<SquaresController> logger) : ControllerBase
    {
        private readonly ISquareService _squaresService = squaresService;
        private readonly IMappingService _mappingService = mappingService;
        private readonly ILogger<SquaresController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> CreateSquare([FromBody] SquareDto squareDto)
        {
            try
            {
                _logger.LogInformation("Creating a new square.");

                var square = _mappingService.MapToEntity(squareDto);
                var result = await _squaresService.CreateSquareAsync(square);

                if (result.Status == INTERNAL_SERVER_ERROR)
                {
                    _logger.LogError("Failed to create square: {Message}", result.Message);
                    return StatusCode((int)result.Status, result.Message);
                }

                if (result.Status != CREATED && result.Status != OK)
                {
                    _logger.LogWarning("Unexpected status when creating square: {Status}", result.Status);
                    return StatusCode((int)result.Status, result.Message);
                }

                _logger.LogInformation("Square created successfully.");
                return CreatedAtAction(nameof(GetAllSquares), new { id = square.Id }, squareDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the square.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSquares()
        {
            try
            {
                _logger.LogInformation("Retrieving all squares.");

                var result = await _squaresService.GetAllSquaresAsync();

                if (result.Status == NOT_FOUND)
                {
                    _logger.LogWarning("No squares found.");
                    return NotFound(result.Message);
                }
                if (result.Status != OK)
                {
                    _logger.LogError("Failed to retrieve squares: {Message}", result.Message);
                    return StatusCode((int)result.Status, result.Message);
                }

                if (result.Data != null)
                {
                    var squareDtos = result.Data.Select(_mappingService.MapToDto);
                    _logger.LogInformation("Retrieved all squares successfully.");
                    return Ok(squareDtos);
                }
                else
                {
                    _logger.LogWarning("No squares found.");
                    return NotFound("No squares found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving squares.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllSquares()
        {
            try
            {
                _logger.LogInformation("Deleting all squares.");

                var result = await _squaresService.DeleteAllSquaresAsync();

                if (result.Status == NOT_FOUND)
                {
                    _logger.LogWarning("No squares found to delete.");
                    return NotFound(result.Message);
                }
                if (result.Status != OK)
                {
                    _logger.LogError("Failed to delete squares: {Message}", result.Message);
                    return StatusCode((int)result.Status, result.Message);
                }

                _logger.LogInformation("All squares deleted successfully.");
                return Ok("All squares deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting squares.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
