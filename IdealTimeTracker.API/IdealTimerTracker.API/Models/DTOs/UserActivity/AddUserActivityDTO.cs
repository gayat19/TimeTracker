namespace IdealTimeTracker.API.Models.DTOs.UserActivity
{
    public class AddUserActivityDTO
    {
        public string? Activity { get; set; }
        public int DurationInMins { get; set; }
        public int? CountPerDay { get; set; }
    }
}
