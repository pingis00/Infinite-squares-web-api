namespace InfiniteSquaresWebAPI.DTOs;

public class SquareDto
{
    public int Id { get; set; }
    public string Color { get; set; } = null!;
    public int Row { get; set; }
    public int Column { get; set; }
}
