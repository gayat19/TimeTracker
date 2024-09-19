using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Utility.Helper
{
    public class VirtualDesktopManager
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const byte
     VK_LWIN = 0x5B;  // Left Windows key
        private const byte VK_CONTROL = 0x11;
        private const byte VK_F4 = 0x73;
        private const uint KEYEVENTF_KEYUP = 0x2;

        public static void CloseVirtualDesktop()
        {
            // Simulate key down events
            keybd_event(VK_LWIN, 0, 0, UIntPtr.Zero);
            keybd_event(VK_CONTROL, 0, 0, UIntPtr.Zero);
            keybd_event(VK_F4, 0, 0, UIntPtr.Zero);

            // Simulate key up events
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            keybd_event(VK_F4, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        public static void CloseAllVirtualDesktops(int length)
        {
            // Looping a high number like 800 might not be ideal. 
            // Consider detecting the actual number of desktops or using a different strategy.
            for (int i = 0; i < length; i++)
            {
                CloseVirtualDesktop();
            }
        }
    }
}
