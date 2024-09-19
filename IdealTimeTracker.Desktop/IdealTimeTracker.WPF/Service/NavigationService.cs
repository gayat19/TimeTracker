using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.ViewModel;

namespace IdealTimeTracker.WPF.Service
{
    public class NavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _currentViewModel;
        public NavigationService(NavigationStore navigationStore,Func<TViewModel> currentViewModel) {
            _navigationStore = navigationStore;
            _currentViewModel = currentViewModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _currentViewModel();
        }
    }
}
