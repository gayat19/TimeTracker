using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Model
{
    public class ApplicationConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Value { get; set; }
    }
}
