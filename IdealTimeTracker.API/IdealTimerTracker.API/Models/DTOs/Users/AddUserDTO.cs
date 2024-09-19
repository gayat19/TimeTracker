using System.ComponentModel.DataAnnotations;

namespace IdealTimeTracker.API.Models.DTOs.Users
{
    public class AddUserDTO
    {
        public string? email { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ReportingTo { get; set; }
        public string? EmpId { get; set; }
        public string? Password { get; set; }

    }
}
