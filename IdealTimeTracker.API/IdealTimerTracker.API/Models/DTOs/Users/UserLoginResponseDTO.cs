namespace IdealTimeTracker.API.Models.DTOs.Users
{
    public class UserLoginResponseDTO
    {
        public string EmpId { get; set; }
        public string? Name { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }

    }
}
