using InfiniteSquaresCore.Interfaces.Repositories;
using InfiniteSquaresCore.Responses;
using System.Linq.Expressions;

namespace InfiniteSquaresInfrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public Task<ResponseResult> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<T>> GetOneAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult> UpdateAsync(Expression<Func<T, bool>> predicate, T updatedEntity)
    {
        throw new NotImplementedException();
    }
}
