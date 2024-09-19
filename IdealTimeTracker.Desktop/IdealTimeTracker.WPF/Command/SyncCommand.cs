using IdealTimeTracker.WPF.Service.Interface;

namespace IdealTimeTracker.WPF.Command
{
    public class SyncCommand : AsyncCommandBase
    {
        public event Action<string>? StatusChanged;
        private readonly ISyncDataService _syncDataService;
        public SyncCommand(ISyncDataService syncDataService)
        {
            _syncDataService = syncDataService;
        }

        protected async override Task ExecuteAsync(object? paramter)
        {
          
            if (await _syncDataService.isInternetAvaiable())
            {
                try
                {
                    await _syncDataService.SyncData();
                    StatusChanged?.Invoke("Data synced successfully. Try later");
                }
                catch
                {
                    StatusChanged?.Invoke("Server error try again");

                }
            }
            else
            {
                StatusChanged?.Invoke("No Internet Connection");
            }

    }
}
}
