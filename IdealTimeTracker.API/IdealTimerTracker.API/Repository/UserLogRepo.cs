using Microsoft.EntityFrameworkCore;
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Repository
{
    public class UserLogRepo :  IBulkRepo<UserLog>
    {
        private readonly UserContext _userContext;
        public UserLogRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<UserLog> Add(UserLog entity)
        {
            if (_userContext.UserLogs == null)
                throw new ContextException("UserLogs context is Empty");
            await _userContext.UserLogs.AddAsync(entity);
            await _userContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> BulkInsert(List<UserLog> entities)
        {
            if (_userContext.UserLogs == null)
                throw new ContextException("UserActivities context is Empty");
            await _userContext.UserLogs.AddRangeAsync(entities);
            await _userContext.SaveChangesAsync();

            return entities.Any();
        }

        public async Task<ICollection<UserLog>> GetAll()
        {
            if (_userContext.UserLogs == null)
                throw new ContextException("UserLogs context is Empty");
            var userLogs = _userContext.UserLogs;
            if (userLogs == null)
                throw new UserLogException("UserLogs not found");
            return await userLogs.ToListAsync();
        }

       
    }
}
