using IdealTimeTracker.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Repository.Interface
{
    public interface IUserRepo
    {
        public User? Get(string id);
        public void Merge(List<User> users);
    }
}
