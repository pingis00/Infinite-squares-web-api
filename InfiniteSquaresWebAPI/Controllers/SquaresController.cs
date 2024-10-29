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

                if (result.Status == CREATED || result.Status == OK)
                {
                    _logger.LogInformation("Square created successfully.");
                    return CreatedAtAction(nameof(GetAllSquares), new { id = square.Id }, squareDto);
                }

                _logger.LogError("Failed to create square: {Message}", result.Message);
                return StatusCode((int)result.Status, result.Message);
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

                if (result.Data == null || !result.Data.Any())
                {
                    _logger.LogInformation("No squares found, returning empty list.");
                    return Ok(new List<SquareDto>());
                }

                var squareDtos = result.Data.Select(_mappingService.MapToDto);
                _logger.LogInformation("Retrieved all squares successfully.");
                return Ok(squareDtos);
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
                    _logger.LogInformation("No squares found to delete, returning success.");
                    return Ok("No squares to delete.");
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
