using IdealTimeTracker.API.Models;

namespace IdealTImeTracker.API.Interfaces
{
    public interface IAllUserRepo
    {
        public Task<List<User>> GetAll();
    }
}
