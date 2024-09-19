using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Userlog;
using IdealTimeTracker.API.Models.DTOs.Users;
using IdealTImeTracker.API.Interfaces;
using IdealTImeTracker.API.Models.DTOs;
using IdealTImeTracker.API.Models.DTOs.Userlog;

namespace IdealTimeTracker.API.Services
{
    public class ManagerAction : IManagerAction
    {
        private readonly IBulkRepo<UserLog> _userLogRepo;
        private readonly IRepo<User,string> _userRepo;
        
        private readonly IRepo<UserActivity,int> _userActivityRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;
        public ManagerAction(IBulkRepo<UserLog> userLogRepo,IRepo<User, string> userRepo, IRepo<UserActivity,int>  userActivityRepo,IApplicationConfigRepo applicationConfigRepo)
        {
            _userRepo = userRepo;
            _userLogRepo = userLogRepo;
            _userActivityRepo = userActivityRepo;
            _applicationConfigRepo = applicationConfigRepo;
        }

        public async Task<UserLogPaginationResponseDTO> GetAllLogsForUser(GetUserLogDTO getUserLogDTO)
        {
            var userlogs = await _userLogRepo.GetAll();
            var user = await _userRepo.GetAll();
            var repostingEmp = user.Where(u => u.ReportingTo == getUserLogDTO.reportingToId).Select(u => new { Name = u.Name , EmpId = u.EmpId}).ToList();
            var userLogs=  repostingEmp
                  .SelectMany(emp => userlogs
                      .Where(log => log.EmpId == emp.EmpId && log.Date >= getUserLogDTO.Start.Date && log.Date <= getUserLogDTO.end.Date)
                       .GroupBy(log => log.Date)
                      .Select(group => new UserLogResponseDTO
                      {
                          Date = group.Key.Date.ToString("dd-MM-yyyy"),
                          EmpId = emp.EmpId,
                          Name = emp.Name,
                          Duration = group.Max(log => log.Duration)  // Handles case where there are no logs for the employee
                      })
                  )
                  .ToList();

            const int pageSize = 5;
            int totalPages = (userLogs.Count + pageSize - 1) / pageSize;
            UserLogPaginationResponseDTO pagination = new UserLogPaginationResponseDTO
            {
                Logs = userLogs.Skip(pageSize * (getUserLogDTO.PageNumber - 1)).Take(pageSize * getUserLogDTO.PageNumber).ToList(),
                TotalPages = totalPages,
                CurrentPage = getUserLogDTO.PageNumber
            };

            return pagination;
                
        
        }


        private async Task<List<UserLogResponseDTO>?> GetAllLogsForUserLog(GetUserLogDTO getUserLogDTO)
        {
            var userlogs = await _userLogRepo.GetAll();
            var user = await _userRepo.GetAll();
            var repostingEmp = user.Where(u => u.ReportingTo == getUserLogDTO.reportingToId).Select(u => new { Name = u.Name, EmpId = u.EmpId }).ToList();
            return repostingEmp
                  .SelectMany(emp => userlogs
                      .Where(log => log.EmpId == emp.EmpId && log.Date >= getUserLogDTO.Start.Date && log.Date <= getUserLogDTO.end.Date)
                      .GroupBy(log => log.Date)
                      .Select(group => new UserLogResponseDTO
                      {
                          Date = group.Key.Date.ToString("dd-MM-yyyy"),
                          EmpId = emp.EmpId,
                          Name = emp.Name,
                          Duration = group.Max(log => log.Duration)  // Handles case where there are no logs for the employee
                      })
                  )
                  .ToList();

           
        }

        public async Task<UserLogConsolidatedPaginationResponseDTO> GetConsolatedLogsForUser(GetUserLogDTO getUserLogDTO)
        {
           
            var allLogs = await GetAllLogsForUserLog(getUserLogDTO);
            var config = await _applicationConfigRepo.GetAll();
            TimeSpan WORKINGHOUR = config.First(x=>x.Name== "WORKING TIME").Value ?? new TimeSpan(0,8,30);
            var consolidatedLogs = allLogs.GroupBy(log => log.EmpId).Select(group => new UserLogConsolidatedResponseDTO
            {
                EmpId = group.Key,
                Name = group.First().Name,
                Days = group.Count(),
                AbsentDuration = new TimeSpan(group.Sum(log => log.Duration.GetValueOrDefault().Ticks > WORKINGHOUR.Ticks ? 0 : WORKINGHOUR.Ticks - log.Duration.GetValueOrDefault().Ticks))
            }).ToList();

            const int Pagesize = 5;
            var totalPages = (consolidatedLogs.Count + Pagesize - 1) / Pagesize;

            UserLogConsolidatedPaginationResponseDTO pagination = new UserLogConsolidatedPaginationResponseDTO
            {
                Logs= consolidatedLogs.Skip(Pagesize*(getUserLogDTO.PageNumber-1)).Take(Pagesize * getUserLogDTO.PageNumber).ToList(),
                TotalPages = totalPages,
                CurrentPage = getUserLogDTO.PageNumber
               
            };

            return pagination;
        }


        public async Task<List<UserLogResponseDTO>?> GetAllLogsForUserExcel(GetUserLogDTO getUserLogDTO)
        {
            var userlogs = await _userLogRepo.GetAll();
            var user = await _userRepo.GetAll();
            var repostingEmp = user.Where(u => u.ReportingTo == getUserLogDTO.reportingToId).Select(u => new { Name = u.Name, EmpId = u.EmpId }).ToList();
            return repostingEmp
                  .SelectMany(emp => userlogs
                      .Where(log => log.EmpId == emp.EmpId && log.Date >= getUserLogDTO.Start.Date && log.Date <= getUserLogDTO.end.Date)
                      .GroupBy(log => log.Date)
                      .Select(group => new UserLogResponseDTO
                      {
                          Date = group.Key.Date.ToString("dd-MM-yyyy"),
                          EmpId = emp.EmpId,
                          Name = emp.Name,
                          Duration = group.Max(log => log.Duration)  // Handles case where there are no logs for the employee
                      })
                  )
                  .ToList();


        }

        public async Task<List<UserLogConsolidatedResponseDTO>> GetConsolatedLogsForUserExcel(GetUserLogDTO getUserLogDTO)
        {
            var allLogs = await GetAllLogsForUserLog(getUserLogDTO);
            var config = await _applicationConfigRepo.GetAll();
            TimeSpan WORKINGHOUR = config.First(x => x.Name == "WORKING TIME").Value ?? new TimeSpan(0, 8, 30);
            var consolidatedLogs = allLogs.GroupBy(log => log.EmpId).Select(group => new UserLogConsolidatedResponseDTO
            {
                EmpId = group.Key,
                Name = group.First().Name,
                Days = group.Count(),
                AbsentDuration = new TimeSpan(group.Sum(log => log.Duration.GetValueOrDefault().Ticks > WORKINGHOUR.Ticks ? 0 : WORKINGHOUR.Ticks - log.Duration.GetValueOrDefault().Ticks))
            }).ToList();
            return consolidatedLogs;
        }
        public async Task<List<UserLogDetailResponseDto>> GetDetailLogsForUser(GetUserLogDetailDto getUserLogDetailDto)
        {
            var activities = await _userActivityRepo.GetAll();
            var userlogs = await _userLogRepo.GetAll();
            return userlogs.Where(log => log.EmpId == getUserLogDetailDto.EmpId && log.Date == getUserLogDetailDto.Date.Date)
                .Select(log => new UserLogDetailResponseDto
                {
                    Id = log.Id,
                    EmpId = log.EmpId,
                    Activity = activities.FirstOrDefault(x=>x.Id == log.ActivityId)?.Activity,
                    Reasons = log.Reason,
                    Duration = log.Duration,
                    ActivityAt = log.ActivityAt,
                    Date = log.Date
                })
                .ToList();

        }
    }
}
