namespace IdealTImeTracker.API.Models.DTOs
{
    public class UserLogConsolidatedResponseDTO
    {
        public string? EmpId { get; set; }
        public string? Name { get; set; }

        public TimeSpan? AbsentDuration { get; set; }
        public int Days { get; set; }

        public string? Status { get; set; } = "Absent/Ideal";
     
    }
}
