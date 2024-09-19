namespace IdealTimeTracker.API.Interfaces
{
    public interface IBaseRepo<T>
    {
        public Task<T> Add(T entity);
        public Task<ICollection<T>> GetAll();
    }
}
