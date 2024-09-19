using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdealTimeTracker.WPF.Utility.Helper
{
    public class VirtualDesktop
    {
        public byte[] VirtualDesktopId { get; set; }
        public Guid VirtualDesktopGuid { get; set; }
        public string Name { get; set; }
        public string Wallpaper { get; set; }
        public bool IsCurrentVirtualDesktop { get; set; }

        public static List<VirtualDesktop> EnumerateVirtualDesktops()
        {
            List<VirtualDesktop> virtualDesktops = new List<VirtualDesktop>();
            //get ids from HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops,VirtualDesktopIDs
            //assume byte length is # of VDesktops * 16
            byte[] virtualDesktopIDs = (byte[])Registry.GetValue(
                    keyName: @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops",
                    valueName: "VirtualDesktopIDs",
                    defaultValue: new byte[0]
                );
            byte[] currentVirtualDesktop = (byte[])Registry.GetValue(
                    keyName: @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops",
                    valueName: "CurrentVirtualDesktop",
                    defaultValue: new byte[0]
                );
            int numberOfVirtualDesktops = virtualDesktopIDs.Length / 16;
            for (int iVD = 0; iVD < numberOfVirtualDesktops; iVD++)
            {
                byte[] virtualDesktopID = new byte[16];
                Array.Copy(
                        sourceArray: virtualDesktopIDs,
                        sourceIndex: iVD * 16,
                        destinationArray: virtualDesktopID,
                        destinationIndex: 0,
                        length: 16
                    );
                Guid virtualDesktopGuid = new Guid(virtualDesktopID);
                //keys for each virtual desktop
                string virtualDesktopName = (string)Registry.GetValue(
                        keyName: String.Format(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops\Desktops\{0:B}", virtualDesktopGuid),
                        valueName: "Name",
                        defaultValue: ""
                    );
                //You should handle null here, which means the desktop key doesn't exist, which never happens in my use-case
                if (virtualDesktopName == "")
                {
                    virtualDesktopName = String.Format("Desktop {0}", iVD + 1);
                }
                string virtualDesktopWallpaper = (string)Registry.GetValue(
                        keyName: String.Format(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VirtualDesktops\Desktops\{0:B}", virtualDesktopGuid),
                        valueName: "Wallpaper",
                        defaultValue: ""
                    );
                virtualDesktops.Add(new VirtualDesktop()
                {
                    VirtualDesktopId = virtualDesktopID,
                    VirtualDesktopGuid = virtualDesktopGuid,
                    Name = virtualDesktopName,
                    Wallpaper = virtualDesktopWallpaper,
                    IsCurrentVirtualDesktop = virtualDesktopID.SequenceEqual(currentVirtualDesktop)
                });
            }
            return virtualDesktops;
        }
    }


}
