using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdealTimeTracker.API.Models
{
    public class UserLog
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public string EmpId { get; set; }
        public User? User { get; set; }

        [ForeignKey("UserActivity")]
        public int? ActivityId { get; set; }
        public string? Reason { get; set; }
        public UserActivity? UserActivity { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ActivityAt { get; set; }
        public DateTime Date { get; set; }

    }
}
