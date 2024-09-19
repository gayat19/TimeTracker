using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdealTimeTracker.API.Models.DTOs.Users
{
    public class UserLoginDTO
    {
        public string? email { get; set; }
        public string? Password { get; set; }

    }
}
