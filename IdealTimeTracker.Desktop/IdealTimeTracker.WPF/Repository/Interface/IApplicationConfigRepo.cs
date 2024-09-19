using IdealTimeTracker.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Repository.Interface
{
    public interface IApplicationConfigRepo
    {
        public List<ApplicationConfiguration>? GetAll();
        public void Merge(List<ApplicationConfiguration> applicationConfigurations);

    }
}
