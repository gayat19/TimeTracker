using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Models;
using IdealTImeTracker.API.Interfaces;
using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;
using Microsoft.EntityFrameworkCore;

namespace IdealTImeTracker.API.Repository
{
    public class ApplicationConfigRepo : IApplicationConfigRepo
    {

        private readonly UserContext _userContext;
        public ApplicationConfigRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<ICollection<ApplicationConfiguration>> GetAll()
        {
            if (_userContext.ApplicationConfigurations == null)
                throw new ContextException("ApplicationConfigurations context is Empty");
            return await _userContext.ApplicationConfigurations.ToListAsync();
        }

     

        public async Task<ApplicationConfiguration> Update(UpdateConfigDTO entity)
        {
            if (_userContext.ApplicationConfigurations == null)
                throw new ContextException("ApplicationConfigurations context is Empty");
            var config = await _userContext.ApplicationConfigurations.FirstOrDefaultAsync(x=>x.Id ==  entity.Id);
            if (config == null)
                throw new ApplicationConfigurationException("Application Configuration is not found");

            config.Value = entity.Value;
            await _userContext.SaveChangesAsync();
            return config;
        }
    }
}
