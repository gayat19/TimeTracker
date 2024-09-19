using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Interfaces
{
    public interface IRepo<T, K> : IBaseRepo<T>
    {
        public Task<T> Get(K id);
        public Task<T> Update (T entity);
        public Task<T> Delete (K id);
    }
}
