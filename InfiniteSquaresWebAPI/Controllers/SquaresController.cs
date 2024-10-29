using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresWebAPI.DTOs;
using InfiniteSquaresWebAPI.Interfaces;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSquare([FromBody] SquareDto squareDto)
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

            throw new InvalidOperationException(result.Message);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SquareDto>>> GetAllSquares()
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllSquares()
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

            throw new InvalidOperationException(result.Message);
        }
    }
}
