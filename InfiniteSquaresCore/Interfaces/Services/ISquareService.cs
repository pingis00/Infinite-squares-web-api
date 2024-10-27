using InfiniteSquaresCore.Models;
using InfiniteSquaresCore.Responses;

namespace InfiniteSquaresCore.Interfaces.Services;

public interface ISquareService
{
    Task<ResponseResult> CreateSquareAsync(Square square);
    Task<ResponseResult<IEnumerable<Square>>> GetAllSquaresAsync();
    Task<ResponseResult> DeleteAllSquaresAsync();
}
