using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Users;

namespace IdealTimeTracker.API.Interfaces
{
    public interface IUserAction 
    {
        public Task<User> UpdatePassword(UpdateUserDTO updateUserDTO);
        public Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);

    }
}
