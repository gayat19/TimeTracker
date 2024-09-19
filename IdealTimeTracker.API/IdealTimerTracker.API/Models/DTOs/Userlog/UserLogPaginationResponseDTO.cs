using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Userlog;
namespace IdealTImeTracker.API.Models.DTOs.Userlog
{
    public class UserLogPaginationResponseDTO
    {
        public List<UserLogResponseDTO>? Logs { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
