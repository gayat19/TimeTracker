namespace IdealTimeTracker.API.Models.DTOs.Users
{
    public class UserReponseDTO
    {
        public string EmpId { get; set; }
        public bool IsActive { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ReportingTo { get; set; }
        public string? PassWord { get; set; }

    }
}
