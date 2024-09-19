using IdealTimeTracker.WPF.Context;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;

namespace IdealTimeTracker.WPF.Repository
{
    public class UserActivityRepo : IUserActivityRepo
    {
        private  UserContextDbFactory _contextDbFactory;
        public UserActivityRepo(UserContextDbFactory contextDbFactory) {
            _contextDbFactory =  contextDbFactory;
        }

        public  List<UserActivity>? GetAllUserActivity()
        {
            UserContext _userContext  = _contextDbFactory.CreateDbContext();
            if(_userContext == null || _userContext.UserActivities == null)
            {
                return null;
            }
            return  _userContext.UserActivities.ToList();
        }

        public UserActivity? GetUserActivity(string activity)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.UserActivities == null)
                {
                    return null;
                }
            return _userContext.UserActivities.Where(x => x.Activity == activity).FirstOrDefault();
        }

        public void Merge(List<UserActivity> userActivities)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            var existingActivities = _userContext.UserActivities.ToList();
                if (existingActivities != null)
            {
                var addedActivities = userActivities.Where((x) => !existingActivities.Select(y => y.Id).Contains(x.Id)).Where((x) => x.IsActive).ToList();
                var updatedActivities = userActivities
                   .Where(x => existingActivities.Select(y=>y.Id).Contains(x.Id) && x.IsActive)
                   .ToList();

                var deletedActivities = existingActivities
                    .Where(x => !userActivities.Any(y => y.Id == x.Id && y.IsActive))
                    .ToList();
                _userContext.AddRange(addedActivities);

                foreach (var activity in deletedActivities)
                {
                    var existing = existingActivities.FirstOrDefault(x => x.Id == activity.Id);
                    if(existing != null)
                         existing.IsActive = false;
                }

                foreach (var activity in updatedActivities)
                {
                  var existing=   existingActivities.FirstOrDefault(x => x.Id == activity.Id) ;
                    if (existing != null)
                    {
                        existing.DurationInMins = activity.DurationInMins;
                        existing.CountPerDay = activity.CountPerDay;
                    }
                }
            }
            _userContext.SaveChanges();
        }
    }
}
