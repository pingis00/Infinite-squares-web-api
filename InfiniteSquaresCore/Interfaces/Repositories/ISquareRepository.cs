using InfiniteSquaresCore.Models;
using InfiniteSquaresCore.Responses;

namespace InfiniteSquaresCore.Interfaces.Repositories;

public interface ISquareRepository
{
    Task<ResponseResult> CreateAsync(Square entity);
    Task<ResponseResult<IEnumerable<Square>>> GetAllAsync();
    Task<ResponseResult> DeleteAsync();
}
