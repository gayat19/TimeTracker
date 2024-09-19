namespace IdealTimeTracker.API.Interfaces
{
    public interface IBulkRepo<T> : IBaseRepo<T>
    {
        public Task<bool> BulkInsert(List<T> entities);
    }
}
