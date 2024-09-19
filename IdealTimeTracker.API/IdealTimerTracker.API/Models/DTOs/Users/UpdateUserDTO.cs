namespace IdealTimeTracker.API.Models.DTOs.Users
{
    public class UpdateUserDTO
    {
        public string EmpId { get; set; }
        public string? ReportingTo { get; set; }
        public string? NewPassword { get; set; }
        public string? Email { get; set; }

        public string? Name { get; set; }

    }

}
