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
    public class ApplicationConfigRepo : IApplicationConfigRepo
    {

        private UserContextDbFactory _contextDbFactory;
        public ApplicationConfigRepo(UserContextDbFactory contextDbFactory)
        {
            _contextDbFactory = contextDbFactory;
        }
        public List<ApplicationConfiguration>? GetAll()
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            if (_userContext == null || _userContext.UserActivities == null)
            {
                return null;
            }
            return _userContext.ApplicationConfigurations.ToList();
        }

        public void Merge(List<ApplicationConfiguration> applicationConfigurations)
        {
            UserContext _userContext = _contextDbFactory.CreateDbContext();
            var existingConfiguration = _userContext.ApplicationConfigurations.ToList();
            if (existingConfiguration != null)
            {
                foreach (var configuration in applicationConfigurations)
                {
                    var existing = existingConfiguration.FirstOrDefault(x => x.Id == configuration.Id);
                    if (existing != null)
                    {
                        existing.Value = configuration.Value;
                    }
                }
            }
            _userContext.SaveChanges();
        }
    }
}
