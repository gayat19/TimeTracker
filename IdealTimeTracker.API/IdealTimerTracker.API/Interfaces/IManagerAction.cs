using IdealTimeTracker.API.Models.DTOs.Userlog;
using IdealTImeTracker.API.Models.DTOs;
using IdealTImeTracker.API.Models.DTOs.Userlog;

namespace IdealTimeTracker.API.Interfaces
{
    public interface IManagerAction
    {
        public Task<UserLogPaginationResponseDTO> GetAllLogsForUser(GetUserLogDTO getUserLogDTO);
        public Task<UserLogConsolidatedPaginationResponseDTO> GetConsolatedLogsForUser(GetUserLogDTO getUserLogDTO);

        public Task<List<UserLogResponseDTO>> GetAllLogsForUserExcel(GetUserLogDTO getUserLogDTO);
        public Task<List<UserLogConsolidatedResponseDTO>> GetConsolatedLogsForUserExcel(GetUserLogDTO getUserLogDTO);
        public Task<List<UserLogDetailResponseDto>> GetDetailLogsForUser(GetUserLogDetailDto getUserLogDetailDto);


    }
}
