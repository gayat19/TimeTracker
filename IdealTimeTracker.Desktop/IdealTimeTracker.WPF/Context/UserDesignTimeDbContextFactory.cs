using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdealTimeTracker.WPF.Context
{
    public class UserDesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            DbContextOptions<UserContext> optionsBuilder = new DbContextOptionsBuilder<UserContext>().UseSqlite("Data Source=User.db").Options;
            return new UserContext(optionsBuilder);
        }
    }
}
