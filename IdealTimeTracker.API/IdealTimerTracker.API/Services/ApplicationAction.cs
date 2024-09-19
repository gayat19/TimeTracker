using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Userlog;
using IdealTImeTracker.API.Interfaces;
using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;

namespace IdealTimeTracker.API.Services
{
    public class ApplicationAction : IApplicationAction
    {

        private readonly IBulkRepo<UserLog> _userLogRepo;
        private readonly IRepo<User, string> _userRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;
        private readonly IAllUserRepo _allUserRepo;


        public ApplicationAction(IBulkRepo<UserLog> userLogRepo,IRepo<User,string> userRepo, IApplicationConfigRepo applicationConfigRepo,IAllUserRepo allUserRepo)
        {
            _applicationConfigRepo = applicationConfigRepo;
            _userLogRepo = userLogRepo;
            _userRepo = userRepo;
            _allUserRepo = allUserRepo;
        }
        public async Task<bool> AddLogs(List<AddUserLogDTO> userLogDTOs)
        {
            if(userLogDTOs.Count == 0) return false;
            var userLogs = userLogDTOs.Select((log) => new UserLog
            {
                Id = log.Id,
                ActivityAt = log.ActivityAt,
                ActivityId = log.ActivityId,
                Date = log.Date.Date,
                Duration =  log.Duration,
                EmpId = log.EmpId,
                Reason = log.Reason
            }).ToList();
            var existingUser = await _userRepo.GetAll();
            var updatelogs = userLogs.Where((log) => existingUser.Select(x => x.EmpId).Contains(log.EmpId)).ToList();
            await _userLogRepo.BulkInsert(updatelogs);
            return true;
        }

        public async Task<List<ApplicationConfigurationResponseDTO>> GetAllConfiguration()
        {
            var configs = (await _applicationConfigRepo.GetAll()).ToList();
            return configs.Select(x => new ApplicationConfigurationResponseDTO { Id = x.Id, Name = x.Name, Value = x.Value }).ToList();
        }

        public async Task<List<User>> GetAllEmployees()
        {
            return (await _allUserRepo.GetAll()).Where(x=>x.Role == "employee").ToList();
        }
    }
}
