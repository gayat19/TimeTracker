using IdealTimeTracker.WPF.Model;

namespace IdealTimeTracker.WPF.Repository.Interface
{
    public interface IUserLogRepo
    {
        public UserLog? AddUserLog(UserLog userLog);
        public UserLog? GetLastLog(string EmpId,DateTime dateTime);
        public List<UserLog>? GetAllLogByDate(string empId,DateTime dateTime);
        public List<UserLog>? GetAllLog(DateTime dateTime);
        public List<UserLog>? Delete(List<UserLog> userLogs);
    }
}
