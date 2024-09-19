using IdealTimeTracker.API.Models.DTOs.Users;

namespace IdealTImeTracker.API.Models.DTOs.Users
{
    public class UserPaginationReponseDto
    {
        public List<UserReponseDTO>? Users { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
