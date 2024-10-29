using InfiniteSquaresCore.Models;
using InfiniteSquaresWebAPI.DTOs;

namespace InfiniteSquaresWebAPI.Interface;

public interface IMappingService
{
    SquareDto MapToDto(Square square);
    Square MapToEntity(SquareDto squareDto);
}
