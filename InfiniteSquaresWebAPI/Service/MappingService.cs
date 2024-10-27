using InfiniteSquaresCore.Interfaces.Services;
using InfiniteSquaresCore.Models;
using InfiniteSquaresWebAPI.DTOs;
using System.Drawing;

namespace InfiniteSquaresInfrastructure.Services;

public class MappingService : IMappingService
{
    public SquareDto MapToDto(Square square)
    {
        return new SquareDto
        {
            Id = square.Id,
            Color = square.Color,
            Row = square.Row,
            Column = square.Column,
        };
    }

    public Square MapToEntity(SquareDto squareDto)
    {
        return new Square
        {
            Id = squareDto.Id,
            Color = squareDto.Color,
            Row = squareDto.Row,
            Column = squareDto.Column
        };
    }
}
