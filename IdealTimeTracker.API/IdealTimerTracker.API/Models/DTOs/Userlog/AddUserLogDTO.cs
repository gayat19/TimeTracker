    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    namespace IdealTimeTracker.API.Models.DTOs.Userlog
    {
        public class AddUserLogDTO
        {
            public Guid Id { get; set; }
            public string? EmpId { get; set; }
            public int? ActivityId { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime ActivityAt { get; set; }
            public String? Reason { get; set; }
            public DateTime Date { get; set; }

    }
}
