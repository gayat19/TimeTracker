namespace IdealTimeTracker.WPF.Store.Interface
{
    public interface IDisplayTimerStore : IDisposable
    {
        void Start();
        void Stop();
        void AddDurationInSec(double number);
        TimeSpan GetDuration();
        void reset();
        event Action<double>? onTimeChanged;


    }
}
