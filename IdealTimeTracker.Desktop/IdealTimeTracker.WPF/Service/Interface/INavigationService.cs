using IdealTimeTracker.WPF.ViewModel;

namespace IdealTimeTracker.WPF.Service.Interface
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
       public void Navigate();
    }
}
