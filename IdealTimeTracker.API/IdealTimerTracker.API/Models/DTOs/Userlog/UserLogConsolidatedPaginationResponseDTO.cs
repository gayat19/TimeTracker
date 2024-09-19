using IdealTimeTracker.API.Models.DTOs.Users;

namespace IdealTImeTracker.API.Models.DTOs.Userlog
{
    public class UserLogConsolidatedPaginationResponseDTO
    {
        public List<UserLogConsolidatedResponseDTO>? Logs { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
