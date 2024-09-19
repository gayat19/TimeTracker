using System.ComponentModel.DataAnnotations;

namespace IdealTimeTracker.WPF.Model
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        public string? Activity { get; set; }
        public int DurationInMins { get; set; }
        public int? CountPerDay { get; set; }
        public bool IsActive { get; set; }

    }
}
