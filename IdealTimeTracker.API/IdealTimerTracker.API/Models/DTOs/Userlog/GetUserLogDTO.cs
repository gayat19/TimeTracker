using System.ComponentModel.DataAnnotations;

namespace IdealTimeTracker.API.Models.DTOs.Userlog
{
    public class GetUserLogDTO
    {
        [Required]
        public string reportingToId { get; set; }
        public DateTime Start { get; set; }
        public DateTime end { get; set; }
        public int PageNumber { get; set; }
    }
}
