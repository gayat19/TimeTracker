using IdealTimeTracker.WPF.Context;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;

namespace IdealTimeTracker.WPF.Repository
{
    public class UserLogRepo : IUserLogRepo
    {
        private UserContextDbFactory _contextDbFactory;
        public UserLogRepo(UserContextDbFactory contextDbFactory)
        {
            _contextDbFactory = contextDbFactory;
        }
        public  UserLog? AddUserLog(UserLog userLog)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.UserLogs == null)
            {
                return null;
            }
             _userContext.UserLogs.Add(userLog);
             _userContext.SaveChanges();
            return userLog;

        }

        public List<UserLog>? Delete(List<UserLog> userLogs)
        {

            UserContext _userContext = _contextDbFactory.CreateDbContext(); 
            if (_userContext == null || _userContext.UserLogs == null)
            {
                return null;
            }
            foreach (var log in userLogs)
            {
                _userContext.UserLogs.Remove(log);
            }
             _userContext.SaveChangesAsync();
            return userLogs;
        }

        public List<UserLog>? GetAllLog(DateTime dateTime)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            var userLogs = _userContext.UserLogs.Where(x => x.Date < dateTime.Date.AddDays(1)).ToList();
            return userLogs.Where(x => x.EmpId != null).ToList();
        }

        public  List<UserLog>? GetAllLogByDate(string empId,DateTime dateTime)
        {
             UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.UserLogs == null)
            {
                return null;
            }
            return _userContext.UserLogs.Where(x => x.EmpId == empId && x.Date == dateTime).ToList();
        }

        public  UserLog? GetLastLog(string EmpId, DateTime dateTime)
        {
             UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.UserLogs == null)
            {
                return null;
            }
             return  _userContext.UserLogs
            .Where(x => x.EmpId == EmpId ) // Ensure the Date comparison is done correctly
            //.Where(x => x.EmpId == EmpId && x.Date == dateTime.Date) // Ensure the Date comparison is done correctly
            .OrderByDescending(x => x.ActivityAt)
            .FirstOrDefault();
        }
    }
}
