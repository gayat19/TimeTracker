using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IdealTimeTracker.WPF.Model
{
    public class UserLog
    {
        [Key]
        public Guid Id { get; set; }
        public string? EmpId { get; set; }

        [ForeignKey("UserActivity")]
        public int? ActivityId { get; set; }
        public UserActivity? UserActivity { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? ActivityAt { get; set; }
        public DateTime? Date { get; set; }
        [AllowNull]
        public string? Reason { get; set; }
       
       

    }
}
