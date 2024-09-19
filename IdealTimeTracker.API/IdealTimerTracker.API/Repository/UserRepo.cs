using Microsoft.EntityFrameworkCore;
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Repository
{
    public class UserRepo : IRepo<User, string>
    {
        private readonly UserContext _userContext;
        public UserRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<User> Add(User entity)
        {
            if(_userContext.Users == null)
               throw new ContextException("User context is Empty");
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.EmpId == entity.EmpId && user.IsActive == true);
            if (user != null)
                throw new UserException("EMPID Already Exists");

            var userDeleted = await _userContext.Users.FirstOrDefaultAsync(user => user.EmpId == entity.EmpId && user.IsActive == false);
            if (userDeleted != null)
                throw new UserException("EMPID Already Taken");
            //var exUser = await _userContext.Users.FirstOrDefaultAsync(user => user.Email == entity.Email);
            //if (exUser != null)
            //    throw new UserException("User already exists");

            await _userContext.Users.AddAsync(entity);
            await _userContext.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Delete(string id)
        {
            if (_userContext.Users == null)
                throw new ContextException("User context is Empty");
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.EmpId == id && user.IsActive == true);
            if (user == null)
                throw new UserException("User not found");
            user.IsActive = false;
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(string id)
        {
            if (_userContext.Users == null)
                throw new ContextException("User context is Empty");
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.EmpId == id && user.IsActive == true);
            if (user == null)
                throw new UserException("User not found");
            return user;
        }


        public async Task<ICollection<User>> GetAll()
        {
           
            if (_userContext.Users == null)
                throw new ContextException("User context is Empty");
            var users = _userContext.Users
                                .Where(u => u.IsActive).ToList(); // Example filtering condition
            if (users == null)
                throw new UserException("User not found");
            return await Task.FromResult(users);
           
        }

        public async Task<User> Update(User entity)
        {
            if (_userContext.Users == null)
                throw new ContextException("User context is Empty");
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.EmpId == entity.EmpId && user.IsActive == true);
            if (user == null)
                throw new UserException("User not found");
           ;
            user.ReportingTo = entity.ReportingTo;
            user.PassWord = entity.PassWord;
            user.Email = entity.Email;
            user.Name = entity.Name;

            await _userContext.SaveChangesAsync();
            return user;
        }
    }
}
