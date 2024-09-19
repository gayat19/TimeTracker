using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Users;

namespace IdealTimeTracker.API.Services
{
    public class UserAction : IUserAction
    {
        private readonly IRepo<User, string> _userRepo;
        private readonly ITokenGenerate _tokenGenerate;
        public UserAction(IRepo<User, string> userRepo, ITokenGenerate tokenGenerate)
        {
            _tokenGenerate = tokenGenerate;
            _userRepo = userRepo;
        }
        public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
        {
           
                var users = await _userRepo.GetAll();
                var user = users.FirstOrDefault(user => user.Email == userLoginDTO.email || user.EmpId == userLoginDTO.email);


                if (user == null)
                {
                    throw new UserException("User not found");
                }

                
                if (userLoginDTO.Password != null )
                {
                   
                    if (!user.PassWord.Equals(userLoginDTO.Password))
                        throw new UserException("Invalid credentials");
                    
                    UserLoginResponseDTO returnUser = new UserLoginResponseDTO();
                    returnUser.EmpId = user.EmpId;
                    returnUser.Name = user.Name;
                    returnUser.Role = user.Role;
                    returnUser.Token = await _tokenGenerate.GenerateJSONWebToken(user);
                    return returnUser;

                }
                throw new UserException("Empty or invalid credentials");
            
        }

        public async Task<User> UpdatePassword(UpdateUserDTO updateUserDTO)
        {
            User user = await _userRepo.Get(updateUserDTO.EmpId);
            if (updateUserDTO.NewPassword == null)
                throw new UserException("New password is empty");
                 user.PassWord = updateUserDTO.NewPassword;
                var existingUser = await _userRepo.Update(user);
                return existingUser;
            }
        }
    }

