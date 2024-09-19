using Microsoft.EntityFrameworkCore;

namespace IdealTimeTracker.WPF.Context
{
    public class UserContextDbFactory
    {
        private string _dbConnectionString;
        public UserContextDbFactory(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }
        public UserContext CreateDbContext()
        {
            DbContextOptions<UserContext> optionsBuilder = new DbContextOptionsBuilder<UserContext>().UseSqlite(_dbConnectionString).Options;
            return new UserContext(optionsBuilder);
        }
    }
}
