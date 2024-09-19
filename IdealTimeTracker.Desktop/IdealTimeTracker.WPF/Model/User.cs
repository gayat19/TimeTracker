using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Model
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

        public string? ReportingTo { get; set; }

    }
}
