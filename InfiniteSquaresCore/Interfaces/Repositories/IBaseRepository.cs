using InfiniteSquaresCore.Responses;
using System.Linq.Expressions;

namespace InfiniteSquaresCore.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<ResponseResult> CreateAsync(T entity);
    Task<ResponseResult<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task<ResponseResult<T>> GetOneAsync(Expression<Func<T, bool>> predicate);
    Task<ResponseResult> UpdateAsync(Expression<Func<T, bool>> predicate, T updatedEntity);
    Task<ResponseResult> DeleteAsync(Expression<Func<T, bool>> predicate);
}
