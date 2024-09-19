using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Command;
using IdealTimeTracker.WPF.Repository.Interface;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;

namespace IdealTimeTracker.WPF.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string _empId = string.Empty;
        public string EmpId
        {
            get { return _empId; }
            set
            {
                _empId = value;
                OnPropertyChanged(nameof(EmpId));
                LoginCommand.EmpId = value;
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                _empId = value;
                OnPropertyChanged(nameof(_password));
                LoginCommand.Password = value;
            }
        }

        public LoginCommand LoginCommand { get; set; }

        private readonly UserStore _userStore;
        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly IUserLogging _userLogging;
        private readonly IUserInputService _userInputService;
        private readonly IActivityTimerStore _activityTimerStore;
        private readonly INavigationService<HomeViewModel> _navigationService;

        public LoginViewModel(INavigationService<HomeViewModel> navigationService, 
                              IDisplayTimerStore displayTimerStore, 
                              UserStore userStore, 
                              IUserLogging userLogging,
                              IUserInputService userInputService,
                              IActivityTimerStore activityTimerStore,
                              PopupWindow popupWindow,
                              IUserRepo userRepo,
                              IApplicationConfigRepo applicationConfigRepo,
                                MainWindow mainWindow
                              )
        {
            _userStore = userStore;
            _userStore._mainWindow = mainWindow;

            _navigationService = navigationService;
            _displayTimerStore = displayTimerStore;
            _userLogging = userLogging;
            _userInputService = userInputService;
            _activityTimerStore = activityTimerStore;
            LoginCommand = new LoginCommand(_displayTimerStore, _navigationService, _userStore, _userLogging, _userInputService, _activityTimerStore, popupWindow, userRepo, applicationConfigRepo);


        }
    }
}
