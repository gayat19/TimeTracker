namespace IdealTimeTracker.API.Models.DTOs.UserActivity
{
    public class UpdateUserActivityDTO
    {
        public int Id { get; set; }
        public int DurationInMins { get; set; }
        public int? CountPerDay { get; set; }
    }
}
