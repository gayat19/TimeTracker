using IdealTimeTracker.WPF.Model;

namespace IdealTimeTracker.WPF.Repository.Interface
{
    public interface IUserActivityRepo
    {
        public UserActivity? GetUserActivity(string activity);
        public List<UserActivity>? GetAllUserActivity();
        public void Merge(List<UserActivity> userActivities);
    }
}
