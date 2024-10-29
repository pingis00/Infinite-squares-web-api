using InfiniteSquaresCore.Models;
using InfiniteSquaresWebAPI.DTOs;

namespace InfiniteSquaresWebAPI.Interfaces;

public interface IMappingService
{
    SquareDto MapToDto(Square square);
    Square MapToEntity(SquareDto squareDto);
}

