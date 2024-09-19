using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Command;
using IdealTimeTracker.WPF.Model;
using IdealTimeTracker.WPF.Store;
using IdealTimeTracker.WPF.Store.Interface;
using IdealTimeTracker.WPF.Utility.Constant;
using System.Collections.ObjectModel;
using System.Windows;

namespace IdealTimeTracker.WPF.ViewModel
{
    public class PopupViewModel : ViewModelBase
    {
        public const int REASON_INDEX = 1;
        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set
            {
                    _index = value;
                    OnPropertyChanged(nameof(_index));
                    ShowReasonInput = _index == REASON_INDEX ? Visibility.Visible : Visibility.Collapsed;
                
            }
        }

        private string? _reason = null;
        public string? Reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }


        private Visibility _showReasonInput = Visibility.Hidden;
        public Visibility ShowReasonInput
        {
            get { return _showReasonInput; }
            set
            {
                _showReasonInput = value;
                OnPropertyChanged(nameof(ShowReasonInput));
            }
        }

        public ObservableCollection<DropdownDto> dropdowns { get; set; }
        public readonly IActivityTimerStore _activityTimerStore;

        private readonly IDisplayTimerStore _displayTimerStore;
        private readonly UserStore _userStore;
        private readonly IUserLogging _userLogging;

        public PopupViewModel(IActivityTimerStore activityTimerStore,
                               IDisplayTimerStore displayTimerStore,
                               UserStore userStore,
                               IUserLogging userLogging
                               ) {
            Index = 0;
            _activityTimerStore = activityTimerStore;
            _displayTimerStore = displayTimerStore;
            _userStore = userStore;

            _userLogging = userLogging;
            dropdowns = new ObservableCollection<DropdownDto>();
           
        }

        public void closePopup()
        {
            _displayTimerStore.Start();
            _activityTimerStore.Start();
            _userStore.SetIdeal(false);
            Index = 0;
        }

        public void submit()
        {

            var duration = dropdowns[Index].Duration;
            _displayTimerStore.Start();
            _displayTimerStore.AddDurationInSec(duration * 60);

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
                    ActivityId = dropdowns[Index].Id,
                    ActivityAt = DateTime.Now,
                    Reason = Reason,
                };

                _userLogging.AddUserLog(userLog);
            }
            else
            {

                var userLog = new UserLog
                {
                    Id = new Guid(),
                    EmpId = _userStore.EmployeeId,
                    Date = lastLog.Date,
                    Duration = _displayTimerStore.GetDuration(),
                    ActivityId = dropdowns[Index].Id,
                    ActivityAt = DateTime.Now,
                    Reason = Reason
                };

                _userLogging.AddUserLog(userLog);
            }


            _activityTimerStore.Start(); 
            _userStore.SetIdeal(false);
        }
        
        public void loaded()
        {
            dropdowns.Clear();
            dropdowns.Add(new DropdownDto { Id = 0, ActivityName = "Select Reason", Duration = 0, RemainingCount = null });

            List<DropdownDto?> dropdown = _userLogging.GetDropdown(_userStore.EmployeeId, _userStore.Date);
            foreach (var item in dropdown)
            {
                if(item!=null)
                    dropdowns.Add(item);
            }
        }

    }

}
