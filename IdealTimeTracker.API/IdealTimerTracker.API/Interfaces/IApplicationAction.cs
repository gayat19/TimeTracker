using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Userlog;
using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;

namespace IdealTimeTracker.API.Interfaces
{
    public interface IApplicationAction
    {
        public Task<bool> AddLogs(List<AddUserLogDTO> userLogDTOs);
        public Task<List<ApplicationConfigurationResponseDTO>> GetAllConfiguration();
        public Task<List<User>> GetAllEmployees();  

    }
}