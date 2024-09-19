using IdealTImeTracker.API.Models;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;

namespace IdealTImeTracker.API.Interfaces
{
    public interface IApplicationConfigRepo
    {
        public Task<ApplicationConfiguration> Update(UpdateConfigDTO entity);
        public Task<ICollection<ApplicationConfiguration>> GetAll();

    }
}
