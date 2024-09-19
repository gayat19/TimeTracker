using IdealTimeTracker.WPF.Store.Interface;
using System.Timers;

namespace IdealTimeTracker.WPF.Store
{
    internal class ActivityTimerStore : IActivityTimerStore
    {
        private double count;
        public event Action<double>? onTimeChanged;
        private readonly System.Timers.Timer _timer;

        public ActivityTimerStore()
        {
            count = 0;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            count++;
            onTimeChanged?.Invoke(count);
        }

        public void reset()
        {
            count = 0;
        }

        public void Start()
        {
           _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer?.Dispose();  
        }
    }
}
