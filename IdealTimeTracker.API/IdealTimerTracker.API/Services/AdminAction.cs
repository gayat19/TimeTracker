using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.UserActivity;
using IdealTimeTracker.API.Models.DTOs.Users;
using IdealTimeTracker.API.Repository;
using IdealTImeTracker.API.Interfaces;
using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;
using IdealTImeTracker.API.Models.DTOs.Users;

namespace IdealTimeTracker.API.Services
{
    public class AdminAction : IAdminAction
    {
        private readonly IRepo<User, string> _userRepo;
        private readonly ITokenGenerate _tokenGenerate;
        private readonly IRepo<UserActivity, int> _userActivityRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;

        public AdminAction(IRepo<User, string> userRepo, ITokenGenerate tokenGenerate, IRepo<UserActivity , int> userActivityRepo, IApplicationConfigRepo applicationConfigRepo)
        {
            _tokenGenerate = tokenGenerate;
            _userRepo = userRepo;
            _userActivityRepo = userActivityRepo;
            _applicationConfigRepo = applicationConfigRepo;
        }
        public async Task<UserActivity> AddActivity(AddUserActivityDTO addUserActivityDTO)
        {
            var userActivity = new UserActivity
            {
                Activity = addUserActivityDTO.Activity,
                CountPerDay = addUserActivityDTO.CountPerDay,
                DurationInMins = addUserActivityDTO.DurationInMins,
                IsActive = true
            };
            return await _userActivityRepo.Add(userActivity);
        }

        public async Task<UserReponseDTO> AddUser(AddUserDTO addUserDTO)
        {
           
            var user = new User
            {
                Role = addUserDTO.Role,
                IsActive = true,
                Name = addUserDTO.Name,
                Email = addUserDTO.email,
                ReportingTo = addUserDTO.ReportingTo,
                EmpId = addUserDTO.EmpId,
                PassWord = addUserDTO.Password,
            };
            //var hmac = new HMACSHA256();
            //user.PasswordKey = hmac.Key;
            //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("PassWord123"));
            var addedUser =  await _userRepo.Add(user);
            return new UserReponseDTO
            {
                Email = addedUser.Email,
                EmpId = addedUser.EmpId,
                IsActive = addedUser.IsActive,
                Name = addedUser.Name,
                ReportingTo = addedUser.ReportingTo,
                Role = addedUser.Role
            };
        }

        public async Task<UserActivity> DeleteActivitybyId(int id)
        {
            return await _userActivityRepo.Delete(id);
        }

        public async Task<UserReponseDTO> DeleteUserbyId(string EmpId)
        {
            var users = await _userRepo.GetAll();
            if (users.Any(x => x.ReportingTo == EmpId))
            {
                throw new UserException("Some users are mapped with this manager");
            }
            var user = await _userRepo.Delete(EmpId);
            return new UserReponseDTO
            {
                Email = user.Email,
                EmpId = user.EmpId,
                IsActive = user.IsActive,
                Name = user.Name,
                ReportingTo = user.ReportingTo,
                Role = user.Role
            };

        }

        public async Task<UserActivity> GetActivitybyId(int id)
        {
            return await _userActivityRepo.Get(id);
        }

       

        public async Task<List<UserActivity>> GetAllActivity()
        {
            var userActivities= await _userActivityRepo.GetAll();
            return userActivities.ToList();
        }

        public async Task<UserPaginationReponseDto> GetAllUser(int PageNumber)
        {
            const int Pagesize = 5;
            var users = await _userRepo.GetAll();
            var totalPages = (users.Count + Pagesize - 1) / Pagesize;

            UserPaginationReponseDto userPaginationReponseDto = new();
            userPaginationReponseDto.Users =  users.Select(user => new UserReponseDTO
            {
                Email = user.Email,
                EmpId = user.EmpId,
                IsActive = user.IsActive,
                Name = user.Name,
                ReportingTo = user.ReportingTo,
                Role = user.Role,
                PassWord = user.PassWord 
            }).Skip(Pagesize * ( PageNumber -1 )).Take(Pagesize).ToList();
            userPaginationReponseDto.TotalPages = totalPages;
            userPaginationReponseDto.CurrentPage = PageNumber;
            return userPaginationReponseDto;
        }

        public async Task<List<UserDropDownDTO>> GetAllUserEmpIds(string manId)
        {
            var userActivities = await _userRepo.GetAll();
            return userActivities.Where((user)=>user.ReportingTo == manId).Select(user=> new UserDropDownDTO { EmpId = user.EmpId,Name = user.Name }).ToList();
        }

        public async Task<List<UserDropDownDTO>> GetAllUserIdbyRole(string role)
        {
            var userActivities = await _userRepo.GetAll();
            return userActivities.Where((user) => user.Role == role).Select(user => new UserDropDownDTO { EmpId = user.EmpId, Name = user.Name }).ToList();
        }

        public async Task<UserReponseDTO> GetUserbyId(string EmpId)
        {
            var user = await _userRepo.Get(EmpId);
            return new UserReponseDTO
            {
                Email = user.Email,
                EmpId = user.EmpId,
                IsActive = user.IsActive,
                Name = user.Name,
                ReportingTo = user.ReportingTo,
                Role = user.Role
            };
        }

        public async Task<UserActivity> UpdateActivity(UpdateUserActivityDTO updateUserActivity)
        {
            var userActivity = await _userActivityRepo.Get(updateUserActivity.Id);
            userActivity.CountPerDay = updateUserActivity.CountPerDay;
            userActivity.DurationInMins = updateUserActivity.DurationInMins;
            return await _userActivityRepo.Update(userActivity);
        }

        public async Task<ApplicationConfiguration> updateApplicationConfiguration(UpdateConfigDTO updateConfigDTO)
        {
                return await _applicationConfigRepo.Update(updateConfigDTO);
        }

        public async Task<UserReponseDTO> UpdateReportingManager(UpdateUserDTO updateUserDTO)
        {
            
            var user = await _userRepo.Get(updateUserDTO.EmpId);
            user.ReportingTo = updateUserDTO.ReportingTo;
            user.PassWord = updateUserDTO.NewPassword;
            user.Email = updateUserDTO.Email;
            user.Name = updateUserDTO.Name;
            var updatedUser = await _userRepo.Update(user);
            return new UserReponseDTO
            {
                Email = updatedUser.Email,
                EmpId = updatedUser.EmpId,
                IsActive = updatedUser.IsActive,
                Name = updatedUser.Name,
                ReportingTo = updatedUser.ReportingTo,
                Role = updatedUser.Role,
                PassWord = updatedUser.PassWord,
               
            };
        }
    }
}
