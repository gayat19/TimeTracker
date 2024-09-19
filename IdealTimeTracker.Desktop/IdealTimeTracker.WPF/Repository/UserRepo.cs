using IdealTimeTracker.WPF.Context;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Repository
{
    public class UserRepo : IUserRepo
    {
        private UserContextDbFactory _contextDbFactory;
        public UserRepo(UserContextDbFactory contextDbFactory)
        {
            _contextDbFactory = contextDbFactory;
        }
        public User? Get(string id)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.Users == null)
            {
                return null;
            }
            var users = _userContext.Users.ToList();
            var user = users.Where(x => x.EmpId == id).ToList().FirstOrDefault();
            return user;
        }

        public void Merge(List<User> users)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            var existingUsers = _userContext.Users.ToList();
            if (existingUsers != null)
            {
                var addedUsers = users.Where((x) => !existingUsers.Select(y => y.EmpId).Contains(x.EmpId)).Where((x) => x.IsActive).ToList();
                var updatedUsers = users
                   .Where(x => existingUsers.Select(y => y.EmpId).Contains(x.EmpId) && x.IsActive)
                   .ToList();

                var deletedUsers = existingUsers
                    .Where(x => !users.Any(y => y.EmpId == x.EmpId && y.IsActive))
                    .ToList();
                _userContext.AddRange(addedUsers);

                foreach (var user in deletedUsers)
                {
                    var existing = existingUsers.FirstOrDefault(x => x.EmpId == user.EmpId);
                    if (existing != null)
                        existing.IsActive = false;
                }

                foreach (var user in updatedUsers)
                {
                    var existing = existingUsers.FirstOrDefault(x => x.EmpId == user.EmpId);
                    if (existing != null)
                    {
                        existing.ReportingTo = user.ReportingTo;
                        existing.PassWord = user.PassWord;
                    }
                }
            }
            _userContext.SaveChanges();
        }
    }
}
