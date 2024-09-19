namespace IdealTimeTracker.WPF.Service.Interface
{
    public interface IUserInputService
    {
        event Action onInput;

        public void Start();
        public void Stop();

    }
}
