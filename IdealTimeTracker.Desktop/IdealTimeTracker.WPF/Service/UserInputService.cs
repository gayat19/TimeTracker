using IdealTimeTracker.WPF.Service.Interface;
using IdealTimeTracker.WPF.Utility.Hook;
using Microsoft.Win32;

namespace IdealTimeTracker.WPF.Service
{
    public class UserInputService : IUserInputService
    {
        public event Action? onInput;
        private ConsoleKey? previousKey;
        private Queue<ConsoleKey> keyQueue = new Queue<ConsoleKey>();

        MouseInput _mouseInput;
        KeyboardInput _keyboardInput;
        private bool isLocked;

        public UserInputService(MouseInput mouseInput, KeyboardInput keyboardInput) {
            _mouseInput = mouseInput;
            _keyboardInput = keyboardInput;
        }

        public void Start()
        {
            _mouseInput.MouseMoved += mouse_Moved;
            _keyboardInput.KeyBoardKeyPressed += Key_Pressed;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }


        public void Stop()
        {
            _mouseInput.MouseMoved -= mouse_Moved;
            _keyboardInput.KeyBoardKeyPressed -= Key_Pressed;
            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
        }

        private void mouse_Moved(object? sender, EventArgs e)
        {
            if (!isLocked)
                onInput?.Invoke();
        }
         
        private void Key_Pressed(object? sender, EventArgs e)
        {
            
            ConsoleKey? currentKey = (sender as KeyboardInput)?.Key;
           
            if(currentKey == null) return; 
            var twoAlgo = twoKeyAlog(currentKey.Value);
            var singleAlgo = singleKeyAlog(currentKey.Value);
            if (twoAlgo || singleAlgo) return;
            if (!isLocked)
                onInput?.Invoke();
        }

        private bool singleKeyAlog(ConsoleKey consoleKey)
        {
            ConsoleKey? currentKey = consoleKey;
            if (previousKey != null && currentKey != null && previousKey == currentKey) return true;
            previousKey = currentKey;
            return false;
        }

        private bool twoKeyAlog(ConsoleKey consoleKey)
        {

            keyQueue.Enqueue(consoleKey);
            if (keyQueue.Count > 4)
            {
                keyQueue.Dequeue();
            }
           return keyQueue.Count == 4 && keyQueue.ElementAt(0) == keyQueue.ElementAt(3) && keyQueue.ElementAt(1) == keyQueue.ElementAt(2);
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    isLocked = true;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    isLocked = false;
                    break;
            }
        }
    }
}
