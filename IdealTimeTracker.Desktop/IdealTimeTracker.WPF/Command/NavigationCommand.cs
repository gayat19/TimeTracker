using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.ViewModel;

namespace IdealTimeTracker.WPF.Command
{
    public class NavigationCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly INavigationService<TViewModel> _navigationService;
        public NavigationCommand(INavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
    
}
