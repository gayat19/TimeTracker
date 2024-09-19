using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;

namespace IdealTimeTracker.WPF.BusinessLogic
{
    public class UserLogging : IUserLogging
    {
        private readonly IUserActivityRepo _userActivityRepo;
        private readonly IUserLogRepo _userLogRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;
        private readonly IUserRepo _userRepo;

        public UserLogging(IUserActivityRepo userActivityRepo, IUserLogRepo userLogRepo, IApplicationConfigRepo applicationConfigRepo)
        {
            _userActivityRepo = userActivityRepo;
            _userLogRepo = userLogRepo;
            _applicationConfigRepo = applicationConfigRepo;

        }

        public UserLog AddUserLog(UserLog userLog)
        {
            return _userLogRepo.AddUserLog(userLog);
        }

        public List<ApplicationConfiguration?> GetApplicationConfigurations()
        {
            return _applicationConfigRepo.GetAll().ToList();
        }

        public List<DropdownDto> GetDropdown(string empId, DateTime dateTime)
        {
            var activities = _userActivityRepo.GetAllUserActivity();
            var userLogs = _userLogRepo.GetAllLogByDate(empId, dateTime.Date);

            var activityCounts = userLogs
                .GroupBy(x => x.ActivityId)
                .Select(g => new { ActivityId = g.Key, Count = g.Count() })
                .ToList();

            List<UserActivity> dropdown = activities.Where((drop) => drop.Id != 2 && drop.Id != 3 && drop.IsActive ==  true).ToList();

            return dropdown.Select((drop) => new DropdownDto
            {
                Duration = drop.DurationInMins,
                ActivityName = drop.Activity,
                Id = drop.Id,
                RemainingCount = drop.CountPerDay - (activityCounts.FirstOrDefault(ac => ac.ActivityId == drop.Id)?.Count ?? 0)
            }).Where((drop) => drop.Id == 1 || drop.Id == 4 || drop.RemainingCount > 0).ToList();

        }

        public  UserLog GetDuration(string EmpId, DateTime dateTime)
        {
            var userLog =  _userLogRepo.GetLastLog(EmpId,dateTime.Date);
            return userLog;
        }

        public User? GetUserByEmpId(string empId)
        {
            return _userRepo.Get(empId);
        }



        //public Task<List<UserLog>> GetRemainingActivity(DateTime dateTime)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
