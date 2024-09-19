namespace IdealTimeTracker.WPF.Service.Interface
{
    public interface ISyncDataService
    {
        Task<bool> isInternetAvaiable();
        Task SyncData();
    }
}
