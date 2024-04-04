using NextUnit.HardwareContext.Microsoft.Win32;
using System.Runtime.InteropServices;
//using System.Windows.Media;

namespace NextUnit.HardwareContext.SystemInformation
{
    public class WpfScreen
    {
        public static IEnumerable<ScreenInfo> AllScreens
        {
            get
            {
                var screens = new List<ScreenInfo>();
                Win32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Win32.RECT lprcMonitor, int dwData) =>
                {
                    var monitorInfo = new Win32.MONITORINFOEX { cbSize = Marshal.SizeOf(typeof(Win32.MONITORINFOEX)) };
                    if (Win32.GetMonitorInfo(hMonitor, ref monitorInfo))
                    {
                        var screenInfo = new ScreenInfo
                        {
                            DeviceName = monitorInfo.szDevice,
                            /*MonitorArea = new Rect(monitorInfo.rcMonitor.Left, monitorInfo.rcMonitor.Top,
                                                   monitorInfo.rcMonitor.Right - monitorInfo.rcMonitor.Left,
                                                   monitorInfo.rcMonitor.Bottom - monitorInfo.rcMonitor.Top),*/
                            /*WorkArea = new Rect(monitorInfo.rcWork.Left, monitorInfo.rcWork.Top,
                                                monitorInfo.rcWork.Right - monitorInfo.rcWork.Left,
                                                monitorInfo.rcWork.Bottom - monitorInfo.rcWork.Top),*/
                            IsPrimary = (monitorInfo.dwFlags & Win32.MONITORINFOF_PRIMARY) == Win32.MONITORINFOF_PRIMARY,
                        };

                        if (screenInfo.IsPrimary)
                        {
                            // If it's the primary screen, get the BitsPerPixel
                            //screenInfo.BitsPerPixel = PixelFormats.Bgra32.BitsPerPixel;
                        }

                        screens.Add(screenInfo);
                    }
                    return true;
                }, 0);

                return screens;
            }
        }

        public class ScreenInfo
        {
            public string DeviceName { get; set; }
            //public Rect MonitorArea { get; set; }
            //public Rect WorkArea { get; set; }
            public bool IsPrimary { get; set; }
            public int BitsPerPixel { get; set; } // New property        }
        }
    }
}
