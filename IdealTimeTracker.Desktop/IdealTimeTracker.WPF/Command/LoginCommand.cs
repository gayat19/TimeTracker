using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Repository.Interface;
using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Constant;
using IdealTimeTracker.WPF.ViewModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace IdealTimeTracker.WPF.Command
{
    public class LoginCommand : CommandBase
    {
        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly INavigationService<HomeViewModel> _navigationService;
        public string? EmpId { get; set; }
        public string? Password { get; set; }

        private readonly UserStore _userStore;
        private readonly IUserLogging _userLogging;
        private readonly IUserInputService _userInputService;
        private readonly IActivityTimerStore _activityTimerStore;
        private readonly PopupWindow _popupWindow;
        private readonly IUserRepo _userRepo;
        private readonly IApplicationConfigRepo _applicationConfigRepo;
        public LoginCommand(IDisplayTimerStore displayTimerStore,
                            INavigationService<HomeViewModel> navigationService,
                            UserStore userStore,
                            IUserLogging userLogging,
                            IUserInputService userInputService,
                            IActivityTimerStore activityTimerStore,
                            PopupWindow popupWindow,
                            IUserRepo userRepo,
                            IApplicationConfigRepo applicationConfigRepo
            ) 
        {
            _applicationConfigRepo = applicationConfigRepo;
            _userRepo = userRepo;
            _popupWindow = popupWindow;
            _activityTimerStore = activityTimerStore;   
            _userInputService = userInputService;
            _userLogging = userLogging;
            _userStore = userStore;
            _navigationService = navigationService;
            _displayTimerStore = displayTimerStore;
        }

        //public override bool CanExecute(object? parameter)
        //{
        //    return EmpId != null && Password != null && EmpId != string.Empty && Password != string.Empty;
        //}

        public override void Execute(object? parameter)
        {

            if (EmpId != null && Password != null)
            {
                var user = _userRepo.Get(EmpId);
                if (user == null)
                {
                    MessageBox.Show("Invalid Employee Id");
                    return;
                }
                    
              
                if (!user.PassWord.Equals(Password))
                {
                    MessageBox.Show("Invalid Credential");
                    return;
                }

               var config = _applicationConfigRepo.GetAll();
                _userStore.EmployeeId = EmpId;
                _userStore.Date = DateTime.Now;
                _userStore.SetIdeal(false);
                var lastLog = _userLogging.GetDuration(EmpId, _userStore.Date.Date);
                _userStore.Duration =  lastLog?.Duration ?? TimeSpan.Zero;
                //_userLogging.AddUserLog();
                _displayTimerStore.Start();
                _activityTimerStore.Start();
                _userInputService.Start();

                var timeDiff = DateTime.Now - lastLog?.ActivityAt;
                if (lastLog == null || Math.Abs((decimal)timeDiff?.TotalSeconds) > (decimal)Constants.timeSpan.TotalSeconds)
                {
                    var userLog = new UserLog
                    {
                        Id = new Guid(),
                        EmpId = EmpId,
                        Date = _userStore.Date.Date,
                        Duration = TimeSpan.Zero,
                        ActivityId = 2,
                        ActivityAt = DateTime.Now,
                    };
                    _displayTimerStore.AddDurationInSec(TimeSpan.Zero.TotalSeconds);


                    _userLogging.AddUserLog(userLog);
                }
                else
                {

                    _displayTimerStore.AddDurationInSec(_userStore.Duration.TotalSeconds);
                    var userLog = new UserLog
                    {
                        Id = new Guid(),
                        EmpId = EmpId,
                        Date = lastLog.Date?.Date,
                        Duration = _displayTimerStore.GetDuration(),
                        ActivityId = 2,
                        ActivityAt = DateTime.Now,
                    }; 


                    _userLogging.AddUserLog(userLog);
                }

                if(lastLog != null && lastLog.ActivityId != 3)
                {
                    _popupWindow.Topmost = true;
                    _popupWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    _popupWindow.Visibility = Visibility.Visible;
                    _popupWindow.IsOpen = true;
                    _activityTimerStore.reset();
                    _userStore.SetIdeal(true);
                    _displayTimerStore.Stop();
                    _activityTimerStore.Stop();
                }

                _navigationService.Navigate();
                _userStore.setAdminConfig(config);

            }

        }
    }
}
