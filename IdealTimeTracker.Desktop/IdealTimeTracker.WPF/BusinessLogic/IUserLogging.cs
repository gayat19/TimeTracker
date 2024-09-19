using IdealTimeTracker.WPF.Model;

namespace IdealTimeTracker.WPF.BusinessLogic
{
    public interface IUserLogging
    {
        public UserLog AddUserLog(UserLog userLog);
        public UserLog GetDuration(string EmpId,DateTime  dateTime);
        public List<DropdownDto?> GetDropdown(string empId, DateTime dateTime);
        public List<ApplicationConfiguration?> GetApplicationConfigurations();
        public User? GetUserByEmpId(string empId);

        //public Task<List<UserLog>> GetRemainingActivity(DateTime dateTime);
    }
}
