using IdealTimeTracker.WPF.Model;

namespace IdealTimeTracker.WPF.Store
{
    public class UserStore
    {
        public  MainWindow _mainWindow;
      
        public string? EmployeeId { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsIdeal { get; set; } = true;
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public TimeSpan WorkingHours { set; get; }
        public TimeSpan IdealTime { set; get; }
        public TimeSpan SyncOne { get; set; }
        public TimeSpan SyncTwo { get; set; }

        public void setAdminConfig(List<ApplicationConfiguration> applicationConfigurations)
        {
            _mainWindow.Topmost = false;
            _mainWindow.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            _mainWindow.WindowState = System.Windows.WindowState.Normal;
            WorkingHours = applicationConfigurations.FirstOrDefault(x => x.Id == (int)ApplicationConfig.WORKING_TIME)?.Value ?? new TimeSpan(8, 30, 0);
            IdealTime = applicationConfigurations.FirstOrDefault(x => x.Id == (int)ApplicationConfig.IDEAL_TIME)?.Value ?? new TimeSpan(0, 5, 0);
            SyncOne = applicationConfigurations.FirstOrDefault(x => x.Id == (int)ApplicationConfig.SYNC_TIME_ONE)?.Value ?? new TimeSpan(4, 0, 0);
            SyncTwo = applicationConfigurations.FirstOrDefault(x => x.Id == (int)ApplicationConfig.SYNC_TIME_TWO)?.Value ?? new TimeSpan(13, 0, 0);


        }
        public void Reset()
        {
            _mainWindow.Topmost = true;
            _mainWindow.WindowStyle = System.Windows.WindowStyle.None;
            _mainWindow.WindowState = System.Windows.WindowState.Maximized;
            EmployeeId = null;
            Duration = TimeSpan.Zero;
            IsIdeal = true;
            Date = DateTime.Now;
        }

        public event Action<bool>? OnIdealChanged;
        public event Action<DateTime>? OnDateChanged;


        public void SetIdeal(bool isIdeal)
        {
            IsIdeal = isIdeal;
            OnIdealChanged?.Invoke(isIdeal);
        }

        public void setDate(DateTime dateTime)
        {
            Date = dateTime;
            OnDateChanged?.Invoke(Date);
        }
    }
}
