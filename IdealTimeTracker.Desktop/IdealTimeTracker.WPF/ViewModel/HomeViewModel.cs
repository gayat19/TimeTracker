using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Command;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Options;
using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;

namespace IdealTimeTracker.WPF.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {

        private string? _time = "00 : 00 : 00";
        public string? Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        } 

        private string? _empId;
        public string? EmpId
        {
            get { return _empId; }
            set
            {
                _empId = value;
                OnPropertyChanged(nameof(EmpId));
            }
        }

        private string? _date;
        public string? Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string? _color;
        public string? Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }


        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly IUserInputService _userInputService;
        private readonly UserStore _userStore;
        private readonly IActivityTimerStore _activityTimerStore;
        INavigationService<LoginViewModel> _navigationService;

        public CancellationTokenSource ShutDown { get; set; } = new CancellationTokenSource();
        public ICommand LogoutCommand { get; set; }

        private readonly PopupWindow _popupWindow;
        private readonly IUserLogging _userLogging;
        private readonly IOptions<AppOption> _options;
        public HomeViewModel(INavigationService<LoginViewModel> navigationService,
                             IDisplayTimerStore displayTimerStore, 
                             UserStore userStore,
                             IActivityTimerStore activityTimerStore,
                             IUserInputService userInputService,
                             PopupWindow popupWindow,
                             IUserLogging userLogging,
                             ModalStore modalStore,
                             IOptions<AppOption> options) {
            _options = options;
            _popupWindow = popupWindow;
            _activityTimerStore = activityTimerStore;
            _userStore = userStore;
            _userStore.OnIdealChanged += Ideal_StatusChanged;
            _userInputService = userInputService;
            _navigationService = navigationService;
            _displayTimerStore = displayTimerStore;

            Date = userStore.Date.ToString("dd/MM/yyyy");
            EmpId = userStore.EmployeeId;
            Color = userStore.IsIdeal ? "Red" : "Green";

            _displayTimerStore.onTimeChanged += DisplayTimerStore_onTimeChanged;
            _activityTimerStore.onTimeChanged += ActivityTimerStore_onTimeChanged;
            _userLogging = userLogging;
            _userInputService.onInput += _userInputService_onInputChanged;
            LogoutCommand = new LogoutCommand(navigationService,userInputService,displayTimerStore,userStore,activityTimerStore,popupWindow,userLogging,modalStore,options);
            _userStore.OnDateChanged += _userStore_OnDateChanged;

        }

        private void _userStore_OnDateChanged(DateTime date)
        {
            Date = date.ToString("dd/MM/yyyy");
        }

        private void Ideal_StatusChanged(bool obj)
        {
            Color =  obj ? "Red" : "Green";
        }

        private void _userInputService_onInputChanged()
        {
            _activityTimerStore.reset();
        }

        private void ActivityTimerStore_onTimeChanged(double obj)
        {
            if (obj == _userStore.IdealTime.TotalSeconds)
            {
                if (!_popupWindow.IsOpen && !this.ShutDown.IsCancellationRequested)
                {
                    if (!this.ShutDown.IsCancellationRequested)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                _popupWindow.Topmost = true;
                                _popupWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                _popupWindow.Visibility = Visibility.Visible;
                                _popupWindow.IsOpen = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while opening popup window: {ex.Message}");
                            }
                        });
                    }
                }
                _activityTimerStore.reset();
                _userStore.SetIdeal(true);
                _displayTimerStore.Stop();
                _activityTimerStore.Stop();

            }
        }

        private void DisplayTimerStore_onTimeChanged(double obj)
        {
            int hour = (int)(obj / 3600) % 24;
            int minutes = (int)(obj / 60) % 60;
            int seconds = (int)obj % 60;
            Time = $" {hour} : {minutes} : {seconds} ";
        }
    }
}
