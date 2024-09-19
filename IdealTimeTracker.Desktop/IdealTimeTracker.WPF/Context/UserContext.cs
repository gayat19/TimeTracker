using IdealTimeTracker.WPF.Model;
using Microsoft.EntityFrameworkCore;

namespace IdealTimeTracker.WPF.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
  
        public DbSet<UserActivity>? UserActivities { get; set; }
        public DbSet<UserLog>? UserLogs { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<ApplicationConfiguration>? ApplicationConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasIndex(u => u.EmpId)  // Create an index for efficiency
               .IsUnique();

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   EmpId = "ADMIN",
                   PassWord = "ADMIN",
                   IsActive = true,
                   Role = "admin",
                   Name = "Admin User",
                   Email = "admin@example.com",
                   ReportingTo = null
               }
           );

            modelBuilder.Entity<UserActivity>().HasData(
                              //new UserActivity { Id = 1, Activity = "none", DurationInMins = 0, CountPerDay = null, IsActive = true },
                              new UserActivity { Id = 2, Activity = "login", DurationInMins = 0, CountPerDay = null, IsActive = true },
                              new UserActivity { Id = 3, Activity = "logout", DurationInMins = 0, CountPerDay = null, IsActive = true, },
                              new UserActivity { Id = 4, Activity = "Others", DurationInMins = 0, CountPerDay = null, IsActive = true, },
                              new UserActivity { Id = 5, Activity = "tea break", DurationInMins = 15, CountPerDay = 2, IsActive = true },
                              new UserActivity { Id = 6, Activity = "lunch break", DurationInMins = 30, CountPerDay = 2, IsActive = true });


            modelBuilder.Entity<ApplicationConfiguration>().HasData(
             new ApplicationConfiguration { Id = 1, Name = "IDEAL TIME", Value = new TimeSpan(0, 1, 0) },
             new ApplicationConfiguration { Id = 2, Name = "WORKING TIME", Value = new TimeSpan(8, 30, 0) },
             new ApplicationConfiguration { Id = 3, Name = "SYNC TIME ONE", Value = new TimeSpan(4, 0, 0) },
             new ApplicationConfiguration { Id = 4, Name = "SYNC TIME TWO", Value = new TimeSpan(13, 0, 0) }
         );

        }
    }
}
