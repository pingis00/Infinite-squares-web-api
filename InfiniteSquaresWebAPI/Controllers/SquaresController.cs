using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresWebAPI.DTOs;
using InfiniteSquaresWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using static InfiniteSquaresCore.Responses.StatusCode;

namespace InfiniteSquaresWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController(
        ISquareService squaresService, 
        IMappingService mappingService, 
        ILogger<SquaresController> logger) : ControllerBase
    {
        private readonly ISquareService _squaresService = squaresService;
        private readonly IMappingService _mappingService = mappingService;
        private readonly ILogger<SquaresController> _logger = logger;

        private const string UnexpectedError = "An unexpected error occurred.";

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSquare([FromBody] SquareDto squareDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(squareDto);

                _logger.LogInformation("Creating new square at position ({Row}, {Column})", squareDto.Row, squareDto.Column);

                var square = _mappingService.MapToEntity(squareDto);
                var result = await _squaresService.CreateSquareAsync(square);

                if (result.Status is CREATED or OK)
                {
                    _logger.LogInformation("Square created successfully.");
                    return CreatedAtAction(nameof(GetAllSquares), new { id = square.Id }, squareDto);
                }

                _logger.LogError("Failed to create square: {Message}", result.Message);
                return StatusCode((int)result.Status, result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating square at position ({Row}, {Column})", squareDto?.Row, squareDto?.Column);
                return StatusCode(500, UnexpectedError);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SquareDto>>> GetAllSquares()
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
                _logger.LogInformation("Retrieved {Count} squares successfully", squareDtos.Count());
                return Ok(squareDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving squares");
                return StatusCode(500, UnexpectedError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllSquares()
        {
            try
            {
                _logger.LogInformation("Deleting all squares.");

                var result = await _squaresService.DeleteAllSquaresAsync();

                if (result.Status == NOT_FOUND)
                {
                    _logger.LogInformation("No squares found to delete");
                    return Ok("No squares to delete.");
                }
                if (result.Status == OK)
                {
                    _logger.LogInformation("All squares deleted successfully");
                    return Ok("All squares deleted successfully.");
                }

                _logger.LogError("Failed to delete squares: {Message}", result.Message);
                return StatusCode((int)result.Status, result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting squares");
                return StatusCode(500, UnexpectedError);
            }
        }
    }
}
