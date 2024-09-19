using IdealTimeTracker.WPF.Store;

namespace IdealTimeTracker.WPF.Command
{
    public class CancelCommand : CommandBase
    {
        private event Action? CancelEvent;

        private readonly ModalStore _modalStore;
        public CancelCommand(ModalStore modalStore)
        {
            _modalStore = modalStore;
        }
        public override void Execute(object? parameter)
        {
            _modalStore.close();
            CancelEvent?.Invoke();
        }
    }
}
