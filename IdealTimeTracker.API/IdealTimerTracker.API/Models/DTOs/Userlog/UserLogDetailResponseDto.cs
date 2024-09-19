using IdealTimeTracker.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdealTImeTracker.API.Models.DTOs.Userlog
{
    public class UserLogDetailResponseDto
    {
        public Guid Id { get; set; }

        public string EmpId { get; set; }
        public string? Activity { get; set; }
        public string? Reasons { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ActivityAt { get; set; }
        public DateTime Date { get; set; }
    }
}
