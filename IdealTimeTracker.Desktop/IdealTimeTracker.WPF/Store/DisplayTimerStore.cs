using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store.Interface;
using System.Timers;
using System.Windows;
namespace IdealTimeTracker.WPF.Store
{
    public class DisplayTimerStore : IDisplayTimerStore, IDisposable
    {
        private const int SecInHour = 3600;
        private const int SecInMinutes = 60;
        private double count;
        private readonly System.Timers.Timer _timer;
        public event Action<double>? onTimeChanged;
        private readonly ISyncDataService _syncDataService;
        private readonly UserStore _userStore;
        private readonly IUserLogging _userLogging;

        public DisplayTimerStore(UserStore userStore,ISyncDataService syncDataService ,IUserLogging userLogging)
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _userStore = userStore;
            _syncDataService = syncDataService;
            _userLogging = userLogging;

        }

        private async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var syncTime = Math.Floor(DateTime.Now.TimeOfDay.TotalSeconds);
            if (syncTime == _userStore.SyncOne.TotalSeconds || syncTime == _userStore.SyncTwo.TotalSeconds)
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    await _syncDataService.SyncData();
                });
            }
            if (_userStore.Date.Date != System.DateTime.Now.Date)
            {

                //count = 0;
                var logout = new UserLog
                {
                    Id = new Guid(),
                    EmpId = _userStore.EmployeeId,
                    Date = _userStore.Date.Date,
                    Duration = _userStore.Duration,
                    ActivityId = 3,
                    ActivityAt = DateTime.Now,
                };

                _userLogging.AddUserLog(logout);
                var lastLog = _userLogging.GetDuration(_userStore.EmployeeId, _userStore.Date.Date);
                _userStore.setDate(System.DateTime.Now.Date);
                _userStore.Duration = GetDuration();

                var login = new UserLog
                {
                    Id = new Guid(),
                    EmpId = _userStore.EmployeeId,
                    Date = lastLog?.Date?.Date,
                    Duration = _userStore.Duration,
                    ActivityId = 2,
                    ActivityAt = DateTime.Now,
                };
                _userLogging.AddUserLog(login);

            }
            else
            {
                count++;
            }
            onTimeChanged?.Invoke((int)count);
        }

        public void AddDurationInSec(double number)
        {
            count += number;
        }


        public TimeSpan GetDuration()
        {
            int hour = (int)(count / SecInHour) % 24;
            int minutes = (int)(count / SecInMinutes) % 60;
            int seconds = (int)count % 60;
            return new TimeSpan(hour, minutes, seconds);
        }
        public void Start()
        {
            _timer.Start();

        }

        public void Stop()
        {
            _timer?.Stop();
        }

        public void reset()
        {
            count = 0;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
