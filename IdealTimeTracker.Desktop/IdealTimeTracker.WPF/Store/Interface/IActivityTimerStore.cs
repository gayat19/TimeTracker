namespace IdealTimeTracker.WPF.Store.Interface
{
    public interface IActivityTimerStore : IDisposable
    {
        void Start();
        void Stop();
        void reset();

        event Action<double>? onTimeChanged;

    }
}
