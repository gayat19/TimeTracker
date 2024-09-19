using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Command;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Options;
using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;

namespace IdealTimeTracker.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool isInternetAvailable;
        public bool IsInternetAvailable
        {
            get { return isInternetAvailable; }
            set
            {
                isInternetAvailable = value;
                OnPropertyChanged(nameof(IsInternetAvailable));
            }
        }

        private readonly ISyncDataService _syncDataService;
        private readonly IDisplayTimerStore _displayTimerService;
        private readonly IActivityTimerStore _activityTimerService;
        private readonly IUserInputService _userInputService;
        public readonly PopupWindow _popupWindow;
        private readonly NavigationStore _navigationStore;

        public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;

        private bool _isModalOpen = false;
        public bool IsModalOpen
        {
            get { return _isModalOpen; }
            set
            {
                _isModalOpen = value;
                OnPropertyChanged(nameof(IsModalOpen));
            }
        }

        public SyncViewModel SyncViewModel { get; init; }

        private readonly IUserLogging _userLogging;
        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly UserStore _userStore;

        private readonly ModalStore _modalStore;

        public ICommand CancelCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        private readonly IOptions<AppOption> _options;
        public MainViewModel(IDisplayTimerStore displayTimerService,
                             IActivityTimerStore activityTimerService,
                             ISyncDataService syncDataService,
                             IUserInputService userInputService,
                             PopupWindow popupWindow,
                             NavigationStore navigationStore,
                             SyncViewModel syncViewModel,
                             IUserLogging userLogging,
                             IDisplayTimerStore displayTimerStore,
                             UserStore userStore,
                             ModalStore modalStore,
                             INavigationService<LoginViewModel> navigationService,
                             IOptions<AppOption> options
            )
        {
            _options = options;
            _modalStore = modalStore;
            _modalStore.onModalOpenChanged += ModalStore_onModalOpenChanged;
            _userStore = userStore;
            _userLogging   = userLogging;
            _displayTimerStore  = displayTimerStore;
            SyncViewModel = syncViewModel;
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += navigation_changed;
            CancelCommand = new CancelCommand(modalStore);
            LogoutCommand = new LogoutCommand(navigationService,userInputService,displayTimerStore,userStore,activityTimerService,popupWindow,userLogging,modalStore, options);
            _syncDataService = syncDataService;
            _popupWindow = popupWindow;
            _userInputService = userInputService;
            _activityTimerService = activityTimerService;
            _displayTimerService = displayTimerService;
            Application.Current.SessionEnding += Current_SessionEnding;
        }

     

        private void ModalStore_onModalOpenChanged(bool obj)
        {
            IsModalOpen = obj;
        }

        private void navigation_changed()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public bool canExit()
        {
            var noExit = _displayTimerService.GetDuration().TotalMinutes < _userStore.WorkingHours.TotalMinutes && CurrentViewModel is HomeViewModel;
            if(noExit)
            {
               _modalStore.open();
            }
            else
            {
                _modalStore.close();
            }
            return !noExit;
        }


        public void CloseWindow()
        {
            this._popupWindow.IsExit = true;
            if (CurrentViewModel is HomeViewModel homeViewModel)
            {

                homeViewModel.ShutDown.Cancel();
            }

        }


        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            
            Logout();
            Application.Current.Shutdown();
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            CloseWindow();
            Logout();
        }

        private void Logout()
        {
            
            var userLog = new UserLog
            {
                Id = new Guid(),
                EmpId = _userStore.EmployeeId,
                Date = _userStore.Date.Date,
                Duration = _displayTimerStore.GetDuration(),
                ActivityId = 3,
                ActivityAt = DateTime.Now,
            };

            _userLogging.AddUserLog(userLog);
            _userInputService.Stop();
            _displayTimerService.Stop();
            _activityTimerService.Stop();
            _displayTimerService.Dispose();
            _activityTimerService.Dispose();

        }

    }
}
