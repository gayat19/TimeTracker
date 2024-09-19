using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Models;
using IdealTImeTracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdealTImeTracker.API.Repository
{
    public class AllUserRepo : IAllUserRepo
    {
        private readonly UserContext _userContext;
        public AllUserRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<List<User>> GetAll()
        {
            

            if (_userContext.Users == null)
                throw new ContextException("User context is Empty");
            var users = await _userContext.Users
                                .ToListAsync(); // Example filtering condition
            if (users == null)
                throw new UserException("User not found");
            return users;

        }
    }
}
