using InfiniteSquaresCore.Models;
using InfiniteSquaresWebAPI.DTOs;

namespace InfiniteSquaresCore.Interfaces.Services;

public interface IMappingService
{
    SquareDto MapToDto(Square square);
    Square MapToEntity(SquareDto squareDto);
}
