using IdealTimeTracker.WPF.BusinessLogic;
using IdealTimeTracker.WPF.Model;

namespace IdealTimeTracker.WPF.Store
{
    public class ModalStore
    {
        private readonly IUserLogging _userLogging;
        private readonly UserStore _userStore;

        private bool _isModalOpen;
        public bool IsModalOpen
        {
            get { return _isModalOpen; }
            set
            {
                _isModalOpen = value;
                onModalOpenChanged?.Invoke(value);
            }
        }

        public event Action<bool>? onModalOpenChanged;

        public ModalStore(IUserLogging userLogging, UserStore userStore) {
            _userLogging = userLogging;
            _userStore = userStore;
        }

        public void open()
        {
            IsModalOpen = true;
        }

        public void close()
        {
            IsModalOpen = false;
        }
        public void logout()
        {
            IsModalOpen = false;
            var userLog = new UserLog
            {
                Id = new Guid(),
                EmpId = _userStore.EmployeeId,
                Date = _userStore.Date.Date,
                Duration = _userStore.Duration,
                ActivityId = 3,
                ActivityAt = DateTime.Now,
            };
            _userLogging.AddUserLog(userLog);
        }
    }


}
