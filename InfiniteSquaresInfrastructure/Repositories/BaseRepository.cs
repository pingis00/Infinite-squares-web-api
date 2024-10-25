using InfiniteSquaresCore.Interfaces.Repositories;

namespace InfiniteSquaresInfrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
}
