using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Constant;
using IdealTimeTracker.WPF.Utility.Options;
using IdealTimeTracker.WPF.ViewModel;
using Microsoft.Extensions.Options;

namespace IdealTimeTracker.WPF.Command
{
    public class LogoutCommand : CommandBase
    {
        private readonly INavigationService<LoginViewModel> _navigationService;
        private readonly IUserInputService _userInputService;
        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly UserStore _userStore;
        private readonly IActivityTimerStore _activityTimerStore;
        private readonly PopupWindow _popupWindow;
        private readonly IUserLogging _userLogging;
        private readonly IOptions<AppOption> _options;

        private readonly ModalStore _modalStore;
        public LogoutCommand(
            INavigationService<LoginViewModel> navigationService, 
                             IUserInputService userInputService, 
                             IDisplayTimerStore displayTimerStore,
                             UserStore userStore,
                             IActivityTimerStore activityTimerStore,
                             PopupWindow popupWindow,
                             IUserLogging userLogging,
                             ModalStore modalStore,
                             IOptions<AppOption> options
                             )
        {
            _modalStore = modalStore;
            _popupWindow = popupWindow;
            _activityTimerStore = activityTimerStore;
            _userStore = userStore;
            _displayTimerStore= displayTimerStore;
            _navigationService = navigationService;
            _userInputService = userInputService;
            _userLogging = userLogging;
            _options = options;

        }
        public override  void Execute(object? parameter)
        {
            if (_displayTimerStore.GetDuration().TotalSeconds < _userStore.WorkingHours.TotalSeconds && !_modalStore.IsModalOpen)
            {
                _modalStore.open();
          }
            else{

                var lastLog = _userLogging.GetDuration(_userStore.EmployeeId, _userStore.Date.Date);
                var timeDiff = DateTime.Now - lastLog?.ActivityAt;
                if (lastLog == null || Math.Abs((decimal)timeDiff?.TotalSeconds) > (decimal)Constants.timeSpan.TotalSeconds)
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
                }
                else
                {

                    var userLog = new UserLog
                    {
                        Id = new Guid(),
                        EmpId = _userStore.EmployeeId,
                        Date = lastLog?.Date?.Date,
                        Duration = _displayTimerStore.GetDuration(),
                        ActivityId = 3,
                        ActivityAt = DateTime.Now,
                    };

                    _userLogging.AddUserLog(userLog);
                }

             

                _popupWindow.Visibility = System.Windows.Visibility.Hidden;
                _popupWindow.IsOpen = false;
                _userStore.SetIdeal(true);

                _activityTimerStore.reset();
                _activityTimerStore.Stop();
                _displayTimerStore.reset();
                _displayTimerStore.Stop();
                _userInputService.Stop();


                _userStore.Reset();

                _navigationService.Navigate();
                _modalStore.close();
            }
        }
    }
}
