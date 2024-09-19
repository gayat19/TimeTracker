namespace IdealTimeTracker.API.Models.DTOs.Userlog
{
    public class UserLogResponseDTO
    {
        public string? EmpId { get; set; }
        public string? Name { get; set; }

        public TimeSpan? Duration { get; set; }
        public String? Date { get; set; }
        public double DurationInMins
        {
            get
            {
                return Duration?.TotalMinutes ?? 0; // Assuming Duration is always a valid object
            }
        }
    }
}
