using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.UserActivity;
using IdealTimeTracker.API.Models.DTOs.Users;
using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;
using IdealTImeTracker.API.Models.DTOs.Users;

namespace IdealTimeTracker.API.Interfaces
{
    public interface IAdminAction
    {
        // User management
        public Task<UserReponseDTO> AddUser(AddUserDTO addUserDTO);
        public Task<UserReponseDTO> DeleteUserbyId(string EmpId);
        public Task<UserReponseDTO> UpdateReportingManager(UpdateUserDTO updateUserDTO);
        public Task<UserReponseDTO> GetUserbyId(string EmpId);
        public Task<UserPaginationReponseDto> GetAllUser(int PageNumber);
        public Task<List<UserDropDownDTO>> GetAllUserEmpIds(string manId);
        public Task<List<UserDropDownDTO>> GetAllUserIdbyRole(string role);

        // Activity management
        public Task<UserActivity> AddActivity(AddUserActivityDTO addUserActivityDTO);
        public Task<UserActivity> UpdateActivity(UpdateUserActivityDTO updateUserActivity);
        public Task<UserActivity> DeleteActivitybyId(int id);
        public Task<List<UserActivity>> GetAllActivity();
        public Task<UserActivity> GetActivitybyId(int id);


        // Application Configuarion

        public Task<ApplicationConfiguration> updateApplicationConfiguration(UpdateConfigDTO updateConfigDTO);


    }
}
