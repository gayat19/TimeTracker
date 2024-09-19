using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IdealTimeTracker.API.Models
{
    public class User   
    {
        [Key]

        public string EmpId { get; set; }
        public string PassWord { get; set; }
        //public byte[]? PasswordHash { get; set; }
        //public byte[]? PasswordKey { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DefaultValue("employee")]
        public string? Role { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        [ForeignKey("ReportingUser")]
        public string? ReportingTo { get; set; }

        [JsonIgnore]
        public User? ReportingUser { get; set; }
    }
}
