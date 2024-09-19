using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Interfaces
{
    public interface ITokenGenerate
    {
        public Task<string> GenerateJSONWebToken(User user);
    }
}
