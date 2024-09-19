using IdealTimeTracker.WPF.Command;

namespace IdealTimeTracker.WPF.ViewModel
{
    public class SyncViewModel : ViewModelBase
    {

        private string _status = string.Empty;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public SyncCommand SyncCommand { get; set; }
        public SyncViewModel(SyncCommand syncCommand)
        {
            Status = "sync your data now";
            SyncCommand = syncCommand;
            syncCommand.StatusChanged += SyncCommand_StatusChanged;

        }
       private void SyncCommand_StatusChanged(string obj)
        {
            Status = obj;
        }
    }
}
