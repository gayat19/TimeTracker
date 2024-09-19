using Microsoft.EntityFrameworkCore;
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Repository
{
    public class UserActivityRepo : IRepo<UserActivity, int>
    {
        private readonly UserContext _userContext;
        public UserActivityRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<UserActivity> Add(UserActivity entity)
        {
            if (_userContext.UserActivities == null)
                throw new ContextException("UserActivities context is Empty");
            await _userContext.UserActivities.AddAsync(entity);
            await _userContext.SaveChangesAsync();

            return entity;
        }



        public async Task<UserActivity> Delete(int id)
        {
            if (_userContext.UserActivities == null)
                throw new ContextException("UserActivities context is Empty");
            var userActivity = await _userContext.UserActivities.FirstOrDefaultAsync(user => user.Id.Equals(id) && user.IsActive == true);
            if (userActivity == null)
                throw new UserActivityException("UserActivities not found");
            userActivity.IsActive = false;
            await _userContext.SaveChangesAsync();
            return userActivity;
        }

        public async Task<UserActivity> Get(int id)
        {
            if (_userContext.UserActivities == null)
                throw new ContextException("UserActivities context is Empty");
            var userActivity = await _userContext.UserActivities.
                                          FirstOrDefaultAsync(user => user.Id.Equals(id) && user.IsActive == true);
            if (userActivity == null)
                throw new UserException("UserActivities not found");
            return userActivity;
        }

        public async Task<ICollection<UserActivity>> GetAll()
        {
            if (_userContext.UserActivities == null)
                throw new ContextException("UserActivities context is Empty");
            var userActivities = _userContext.UserActivities
                             .Where(u => u.IsActive); // Example filtering condition
            if (userActivities == null)
                throw new UserException("UserActivities not found");
            return await userActivities.ToListAsync() ;
        }

        public async Task<UserActivity> Update(UserActivity entity)
        {
            if (_userContext.UserActivities == null)
                throw new ContextException("UserActivities context is Empty");
            var userActivity = await _userContext.UserActivities
                                                 .FirstOrDefaultAsync(user => user.Id == entity.Id && user.IsActive == true);
            if (userActivity == null)
                throw new UserException("UserActivities not found");
            ;
            userActivity.DurationInMins = entity.DurationInMins;
            userActivity.CountPerDay = entity.CountPerDay;
            await _userContext.SaveChangesAsync();
            return userActivity;
        }
    }
}
