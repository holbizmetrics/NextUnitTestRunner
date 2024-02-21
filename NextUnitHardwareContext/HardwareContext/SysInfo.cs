using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
//using PowerLineStatus = System.Windows.PowerLineStatus;
using System.ServiceProcess;
using System.DirectoryServices;
/* Unmerged change from project 'NextUnitHardwareContext (net7.0-windows)'
Before:
using System.Reflection;
After:
using System.Reflection;
using NextUnitHardwareContext;
using NextUnitHardwareContext.SystemInformation;
*/
using System.Reflection;
using NextUnit.HardwareContext.SystemInformation.SystemInformation.SystemInformation;
using System.Management;
using Microsoft.Management.Infrastructure;
using static NextUnit.HardwareContext.Microsoft.Win32.Win32;
using NextUnit.HardwareContext.Extensions;
using System.Security;

namespace NextUnit.HardwareContext.SystemInformation
{
    public static class SysInfo
    {
        public static Dictionary<ManagementObject, PropertyDataCollection> GetRawSystemData()
        {
            Dictionary<ManagementObject, PropertyDataCollection> classObjects = new Dictionary<ManagementObject, PropertyDataCollection>();

            // Create a ManagementObjectSearcher object
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Meta_Class");

            // Loop through each object in the searcher
            foreach (ManagementObject queryObj in searcher.Get())
            {
                //    // Display the class
                //    Trace.WriteLine("-----------------------------------");
                //    Trace.WriteLine("Meta_Class instance");
                //    Trace.WriteLine("-----------------------------------");
                //    Trace.WriteLine($"ClassName: {queryObj["__CLASS"]}");

                //    // Get the properties of the class
                PropertyDataCollection properties = queryObj.Properties;

                //    // Loop through each property
                //    foreach (PropertyData property in properties)
                //    {
                //        // Display the property
                //        Trace.WriteLine($"Property: {property.Name}, Value: {property.Value}");
                //    }
                classObjects.Add(queryObj, properties);
            }
            return classObjects;
        }

        public static List<CimInstance> GetCurrentResolution()
        {
            return QueryCIMInstances("Win32_VideoController");
        }

        [SecurityCritical]
        public static string GetWindowsProductKey()
        {
            return QueryCIMInstances("SoftwareLicensingService")[0].CimInstanceProperties["OA3xOriginalProductKey"].ToString();
        }

        public static List<CimInstance> GetLogicalDisks()
        {
            return QueryCIMInstances("Win32_LogicalDisk");
        }

        public static List<CimInstance> GetDiskPartitions()
        {
            return QueryCIMInstances("CIM_LogicalDisk");
        }

        public static List<CimInstance> GetNetworkAdapters()
        {
            return QueryCIMInstances("Win32_NetworkAdapter");
        }

        public static List<CimInstance> GetOperatingSystemInformation()
        {
            return QueryCIMInstances("Win32_OperatingSystem");
        }

        public static List<CimInstance> GetDiskDrives()
        {
            return QueryCIMInstances("Win32_DiskDrive");
        }

        public static List<CimInstance> GetUSBDevices()
        {
            return QueryCIMInstances("Win32_PnPEntity");
        }

        public static List<CimInstance> GetNetworkShares()
        {
            return QueryCIMInstances("Win32_Share");
        }

        public static List<CimInstance> GetHardware()
        {
            return QueryCIMInstances("Win32_ComputerSystem");
        }

        public static List<CimInstance> GetSoftwareLicensing()
        {
            return QueryCIMInstances("SoftwareLicensingProduct");
        }

        public static List<CimInstance> GetNetRoute()
        {
            throw new NotImplementedException();
        }
        public static DateTime GetUpTime()
        {
            return DateTime.Parse(QueryCIMInstances("Win32_OperatingSystem").ToList()[0].CimInstanceProperties["LastBootUpTime"].Value.ToString());
        }

        public static List<CimInstance> GetGraphicsCards()
        {
            return QueryCIMInstances("win32_VideoController");
        }
        public static List<CimInstance> GetUserAccounts()
        {
            return QueryCIMInstances("Win32_UserAccount");
        }
        public static List<CimInstance> GetSupportedResolutions()
        {
            return QueryCIMInstances("CIM_VideoControllerResolution");
        }
        public static List<CimInstance> GetHotFixes()
        {
            return QueryCIMInstances("Win32_QuickFixEngineering");
        }

        public static List<CimInstance> GetStartUpApplications()
        {
            return QueryCIMInstances("Win32_StartupCommand");
        }
        public static List<CimInstance> GetBIOSInfo()
        {
            return QueryCIMInstances("Win32_BIOS");
        }
        public static List<CimInstance> GetProcessorInfo()
        {
            return QueryCIMInstances("Win32_Processor");
        }
        public static List<CimInstance> GetDisplayInfo()
        {
            return QueryCIMInstances("Win32_VideoController");
        }
        public static List<CimInstance> GetLogonSessionInformation()
        {
            return QueryCIMInstances("Win32_LogonSession");
        }

        public static List<CimInstance> GetLoggedInUsers()
        {
            return QueryCIMInstances("Win32_ComputerSystem");
        }

        #region SubSpaces
#if NET48_OR_GREATER
        public static System.Drawing.Printing.PrinterSettings.StringCollection PrinterSettings { get { return System.Drawing.Printing.PrinterSettings.InstalledPrinters; } }
#endif
        public static Services Services { get; private set; } = new Services();
        public static Process[] Processes { get { return Process.GetProcesses(); } }
        #endregion SubSpaces

        private static SYSTEM_INFO systemInfo = SYSTEM_INFO.EMPTY;
        //derived from SystemParameters.

#if SYSTEM_WINDOWS
        public static double PrimaryScreenWidth { get { return SystemParameters.PrimaryScreenWidth; } }
        public static double PrimaryScreenHeight { get { return SystemParameters.PrimaryScreenHeight; } }
        public static PowerLineStatus PowerLineStatus { get { return SystemParameters.PowerLineStatus; } }
        public static int WheelScrollLines { get { return SystemParameters.WheelScrollLines; } }
        public static double WindowCaptionButtonHeight { get { return SystemParameters.WindowCaptionButtonHeight; } }
        public static double WindowCaptionButtonWidth { get { return SystemParameters.WindowCaptionButtonWidth; } }
        public static double WindowCaptionHeight { get { return SystemParameters.WindowCaptionHeight; } }
        public static CornerRadius WindowCornerRadius { get { return SystemParameters.WindowCornerRadius; } }
        public static Process CurrentProcess { get { return Process.GetCurrentProcess(); } }
        public static Dispatcher CurrentDispatcher { get { return Dispatcher.CurrentDispatcher; } }
        public static Thread CurrentThread { get { return Thread.CurrentThread; } }
        public static double CaretWidth { get { return SystemParameters.CaretWidth; } }
        public static bool ClientAreaAnimation { get { return SystemParameters.ClientAreaAnimation; } }
        public static bool ComboBoxAnimation { get { return SystemParameters.ComboBoxAnimation; } }
        public static PopupAnimation ComboBoxPopupAnimation { get { return SystemParameters.ComboBoxPopupAnimation; } }
        public static double CursorHeight { get { return SystemParameters.CursorHeight; } }
        public static bool CursorShadow { get { return SystemParameters.CursorShadow; } }
        public static double CursorWidth { get { return SystemParameters.CursorWidth; } }
        public static bool DragFullWindows { get { return SystemParameters.DragFullWindows; } }
        public static bool DropShadow { get { return SystemParameters.DropShadow; } }
        public static double FixedFrameHorizontalBorderHeight { get { return SystemParameters.FixedFrameHorizontalBorderHeight; } }
        public static double FixedFrameVerticalBorderWidth { get { return SystemParameters.FixedFrameVerticalBorderWidth; } }
        public static bool FlatMenu { get { return SystemParameters.FlatMenu; } }
        public static double FocusBorderHeight { get { return SystemParameters.FocusBorderHeight; } }
        public static double FocusBorderWidth { get { return SystemParameters.FocusBorderWidth; } }
        public static double FocusHorizontalBorderHeight { get { return SystemParameters.FocusHorizontalBorderHeight; } }
        public static double FocusVerticalBorderWidth { get { return SystemParameters.FocusVerticalBorderWidth; } }
        public static int ForegroundFlashCount { get { return SystemParameters.ForegroundFlashCount; } }
        public static double FullPrimaryScreenHeight { get { return SystemParameters.FullPrimaryScreenHeight; } }
        public static double FullPrimaryScreenWidth { get { return SystemParameters.FullPrimaryScreenWidth; } }
        public static bool GradientCaptions { get { return SystemParameters.GradientCaptions; } }
        public static bool HighContrast { get { return SystemParameters.HighContrast; } }
        public static double HorizontalScrollBarButtonWidth { get { return SystemParameters.HorizontalScrollBarButtonWidth; } }
        public static double HorizontalScrollBarHeight { get { return SystemParameters.HorizontalScrollBarHeight; } }
        public static double HorizontalScrollBarThumbWidth { get { return SystemParameters.HorizontalScrollBarThumbWidth; } }
        public static bool HotTracking { get { return SystemParameters.HotTracking; } }
        public static double IconGridHeight { get { return SystemParameters.IconGridHeight; } }
        public static double IconGridWidth { get { return SystemParameters.IconGridWidth; } }
        public static double IconHeight { get { return SystemParameters.IconHeight; } }
        public static double IconHorizontalSpacing { get { return SystemParameters.IconHorizontalSpacing; } }
        public static bool IconTitleWrap { get { return SystemParameters.IconTitleWrap; } }
        public static double IconVerticalSpacing { get { return SystemParameters.IconVerticalSpacing; } }
        public static double IconWidth { get { return SystemParameters.IconWidth; } }
        public static double MenuButtonWidth { get { return SystemParameters.MenuButtonWidth; } }
        public static double MinimumWindowWidth { get { return SystemParameters.MinimumWindowWidth; } }
        public static double MinimumWindowTrackWidth { get { return SystemParameters.MinimumWindowTrackWidth; } }
        public static double MaximumWindowTrackWidth { get { return SystemParameters.MaximumWindowTrackWidth; } }
        public static int Border { get { return SystemParameters.Border; } }
        public static double BorderWidth { get { return SystemParameters.BorderWidth; } }
        public static double CaptionHeight { get { return SystemParameters.CaptionHeight; } }
        public static double CaptionWidth { get { return SystemParameters.CaptionWidth; } }
        public static bool IsGlassEnabled { get { return SystemParameters.IsGlassEnabled; } }
        public static bool IsImmEnabled { get { return SystemParameters.IsImmEnabled; } }
        public static bool IsMediaCenter { get { return SystemParameters.IsMediaCenter; } }
        public static bool IsMenuDropRightAligned { get { return SystemParameters.IsMenuDropRightAligned; } }
        public static bool IsMiddleEastEnabled { get { return SystemParameters.IsMiddleEastEnabled; } }
        public static bool IsMousePresent { get { return SystemParameters.IsMousePresent; } }
        public static bool IsMouseWheelPresent { get { return SystemParameters.IsMouseWheelPresent; } }
        public static bool IsPenWindows { get { return SystemParameters.IsPenWindows; } }
        public static bool IsRemotelyControlled { get { return SystemParameters.IsRemotelyControlled; } }
        public static bool IsRemoteSession { get { return SystemParameters.IsRemoteSession; } }
        public static bool IsSlowMachine { get { return SystemParameters.IsSlowMachine; } }
        public static bool IsTablePC { get { return SystemParameters.IsTabletPC; } }
        public static double KanjiWindowHeight { get { return SystemParameters.KanjiWindowHeight; } }
        public static int KeyboardDelay { get { return SystemParameters.KeyboardDelay; } }
        public static bool KeyboardPreference { get { return SystemParameters.KeyboardPreference; } }
        public static bool KeyboardCues { get { return SystemParameters.KeyboardCues; } }
        public static int KeyboardSpeed { get { return SystemParameters.KeyboardSpeed; } }
        public static bool ListBoxSmoothScrolling { get { return SystemParameters.ListBoxSmoothScrolling; } }
        public static double MaximizedPrimaryScreenHeight { get { return SystemParameters.MaximizedPrimaryScreenHeight; } }
        public static double MaximizedPrimaryScreenWidth { get { return SystemParameters.MaximizedPrimaryScreenWidth; } }
        public static double MaximumWindowTrackHeight { get { return SystemParameters.MaximumWindowTrackHeight; } }
        public static double MaximumdWin { get { return SystemParameters.MaximumWindowTrackHeight; } }
        public static double MaximumWindowTrackWith { get { return SystemParameters.MaximumWindowTrackWidth; } }
        public static bool MenuAnimation { get { return SystemParameters.MenuAnimation; } }
        public static double MenuBarHeight { get { return SystemParameters.MenuBarHeight; } }
        public static double MenuButtonHeight { get { return SystemParameters.MenuButtonHeight; } }
        public static double MenuCheckmarkHeight { get { return SystemParameters.MenuCheckmarkHeight; } }
        public static double MenuCheckmarkWidth { get { return SystemParameters.MenuCheckmarkWidth; } }
        public static bool MenuDropAlignment { get { return SystemParameters.MenuDropAlignment; } }
        public static bool MenuFade { get { return SystemParameters.MenuFade; } }
        public static double MenuHeight { get { return SystemParameters.MenuHeight; } }
        public static double MenuWidth { get { return SystemParameters.MenuWidth; } }
        public static PopupAnimation MenuPopupAnimation { get { return SystemParameters.MenuPopupAnimation; } }
        public static int MenuShowDelay { get { return SystemParameters.MenuShowDelay; } }
        public static bool MinimizeAnimation { get { return SystemParameters.MinimizeAnimation; } }
        public static double MinimizedGridHeight { get { return SystemParameters.MinimizedGridHeight; } }
        public static double MinimizedGridWidth { get { return SystemParameters.MinimizedGridWidth; } }
        public static double MinimizedWindowWidth { get { return SystemParameters.MinimizedWindowWidth; } }
        public static double MinimumHorizontalDragDistance { get { return SystemParameters.MinimumHorizontalDragDistance; } }
        public static double MinimumVerticalDragDistance { get { return SystemParameters.MinimumVerticalDragDistance; } }
        public static double MinimumWindowHeight { get { return SystemParameters.MinimumWindowHeight; } }
        public static double MinimumWindowTrackHeight { get { return SystemParameters.MinimumWindowTrackHeight; } }
        public static double MinimizedWindowHeight { get { return SystemParameters.MinimizedWindowHeight; } }
        public static double MouseHoverHeight { get { return SystemParameters.MouseHoverHeight; } }
        public static TimeSpan MouseHoverTime { get { return SystemParameters.MouseHoverTime; } }
        public static double MouseHoverWidth { get { return SystemParameters.MouseHoverWidth; } }
        public static double ResizeFrameHorizontalBorderHeight { get { return SystemParameters.ResizeFrameHorizontalBorderHeight; } }
        public static double ResizeFrameVerticalBorderWidth { get { return SystemParameters.ResizeFrameVerticalBorderWidth; } }
        public static double ScrollHeight { get { return SystemParameters.ScrollHeight; } }
        public static double ScrollWidth { get { return SystemParameters.ScrollWidth; } }
        public static bool SelectionFade { get { return SystemParameters.SelectionFade; } }
        public static bool ShowSounds { get { return SystemParameters.ShowSounds; } }
        public static double SmallCaptionHeight { get { return SystemParameters.SmallCaptionHeight; } }
        public static double SmallCaptionWidth { get { return SystemParameters.SmallCaptionWidth; } }
        public static double SmallIconHeight { get { return SystemParameters.SmallIconHeight; } }
        public static double SmallIconWidth { get { return SystemParameters.SmallIconWidth; } }
        public static double SmallWindowCaptionButtonHeight { get { return SystemParameters.SmallWindowCaptionButtonHeight; } }
        public static double SmallWindowCaptionButtonWidth { get { return SystemParameters.SmallWindowCaptionButtonWidth; } }
        public static bool SnapToDefaultButton { get { return SystemParameters.SnapToDefaultButton; } }
        public static bool StylusHotTracking { get { return SystemParameters.StylusHotTracking; } }
        public static bool SwapButtons { get { return SystemParameters.SwapButtons; } }
        public static double ThickHorizontalBorderHeight { get { return SystemParameters.ThickHorizontalBorderHeight; } }
        public static double ThickVerticalBorderWidth { get { return SystemParameters.ThickVerticalBorderWidth; } }
        public static double ThinHorizontalBorderHeight { get { return SystemParameters.ThinHorizontalBorderHeight; } }
        public static double ThinVerticalBorderWidth { get { return SystemParameters.ThinVerticalBorderWidth; } }
        public static bool ToolTipAnimation { get { return SystemParameters.ToolTipAnimation; } }
        public static bool ToolTipFade { get { return SystemParameters.ToolTipFade; } }
        public static PopupAnimation ToolTipPopupAnimation { get { return SystemParameters.ToolTipPopupAnimation; } }
        public static string UxThemeColor { get { return SystemParameters.UxThemeColor; } }
        public static double VerticalScrollBarButtonHeight { get { return SystemParameters.VerticalScrollBarButtonHeight; } }
        public static double VerticalScrollBarThumbHeight { get { return SystemParameters.VerticalScrollBarThumbHeight; } }
        public static double VerticalScrollBarWidth { get { return SystemParameters.VerticalScrollBarWidth; } }
        public static double VirtualScreenHeight { get { return SystemParameters.VirtualScreenHeight; } }
        public static double VirtualScreenLeft { get { return SystemParameters.VirtualScreenLeft; } }
        public static double VirtualScreenTop { get { return SystemParameters.VirtualScreenTop; } }
        public static double VirtualScreenWidth { get { return SystemParameters.VirtualScreenWidth; } }
        public static System.Windows.Media.Brush WindowGlassBrush { get { return SystemParameters.WindowGlassBrush; } }
        public static System.Windows.Media.Color WindowGlassColor { get { return SystemParameters.WindowGlassColor; } }
        public static Thickness WindowNonClientFrameThickness { get { return SystemParameters.WindowNonClientFrameThickness; } }
        public static Thickness WindowResizeBorderThickness { get { return SystemParameters.WindowResizeBorderThickness; } }
        public static Rect WorkArea { get { return SystemParameters.WorkArea; } }
#endif

        //Derived from Environment
        public static string CommandLine { get { return Environment.CommandLine; } }
        public static string[] GetCommandLineArgs() { return Environment.GetCommandLineArgs(); }
        public static string GetEnvironmentVariable(string variable) { return Environment.GetEnvironmentVariable(variable); }
        public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target) { return Environment.GetEnvironmentVariable(variable, target); }
        public static IDictionary GetEnvironmentVariables() { return Environment.GetEnvironmentVariables(); }
        public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target) { return Environment.GetEnvironmentVariables(target); }
        public static string[] GetLogicalDrives() { return Environment.GetLogicalDrives(); }
        public static string StrackTrace { get { return Environment.StackTrace; } }
        public static string NewLine { get { return Environment.NewLine; } }
        public static Version Version { get { return Environment.Version; } }
        public static string MachineName { get { return Environment.MachineName; } }
        public static int ExitCode { get { return Environment.ExitCode; } }
        public static string CurrentDirectory { get { return Environment.CurrentDirectory; } }
        public static IDictionary EnvironmentVariables { get { return Environment.GetEnvironmentVariables(); } }
        public static OperatingSystem OperatingSystem { get { return Environment.OSVersion; } }
        public static string SystemDirectory { get { return Environment.SystemDirectory; } }
        public static bool HasShutdownStarted { get { return Environment.HasShutdownStarted; } }
        public static bool Is64BitOperatingSystem { get { return Environment.Is64BitOperatingSystem; } }
        public static bool Is64BitProcess { get { return Environment.Is64BitProcess; } }
        public static int ProcessorCount { get { return Environment.ProcessorCount; } }
        public static int SystemPageSize { get { return Environment.SystemPageSize; } }
        public static long WorkingSet { get { return Environment.WorkingSet; } }
        public static string StackTrace { get { return Environment.StackTrace; } }
        public static int TickCount { get { return Environment.TickCount; } }
        public static string UserName { get { return Environment.UserName; } }
        public static string UserDomainName { get { return Environment.UserDomainName; } }
        public static bool UserInteractive { get { return Environment.UserInteractive; } }
        public static int CurrentManagedThreadId { get { return Environment.CurrentManagedThreadId; } }
        public static uint AllocationGranularity { get { return systemInfo.allocationGranularity; } }
        public static ushort ProcessorLevel { get { return systemInfo.processorLevel; } }
        public static uint ProcessorType { get { return systemInfo.processorType; } }
        public static object ProcessorUsage
        {
            get
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                var cpuTimes = searcher.Get()
                    .Cast<ManagementObject>()
                    .Select(mo => new
                    {
                        Name = mo["Name"],
                        Usage = mo["PercentProcessorTime"]
                    }
                    )
                    .ToArray();
                return cpuTimes;
            }
        }

        public static DriveInfo[] DriveInfos { get { return DriveInfo.GetDrives(); } }

        //TODO: use information from here, that seems to be more correct: https://learn.microsoft.com/de-de/dotnet/api/system.net.networkinformation.networkinterface?view=net-7.0&devlangs=csharp&f1url=%3FappId%3DDev16IDEF1%26l%3DDE-DE%26k%3Dk(System.Net.NetworkInformation.NetworkInterface)%3Bk(DevLang-csharp)%26rd%3Dtrue
        public static List<string> NetworkInterfaces { get { return NetworkInfo.NetWorkInterfaces(); } }

        public static string ComputerName
        {
            get
            {
                string nameFromEnvironment = Environment.MachineName;
                string nameFromDNS = System.Net.Dns.GetHostName();
                return nameFromEnvironment;
            }
        }

        public static string OSFriendlyName
        {
            get
            {
                string result = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
                foreach (ManagementObject os in searcher.Get())
                {
                    result = os["Caption"].ToString();
                    break;
                }

                return result;
            }
        }

        public static string RegisteredOrganization
        {
            get { return GetValue("Win32_OperatingSystem", "RegisteredOrganization"); }
        }

        public static string RegisteredUser
        {
            get { return GetValue("Win32_OperatingSystem", "RegisteredUser"); }
        }

        public static string OSManufacturer
        {
            get { return GetValue("Win32_ComputerSystem", "Manufacturer"); }
        }

        public static List<PropertyData> GetInformationRestricted(string query, string propertyName)
        {
            return GetInformationRestricted(null, query, propertyName);
        }

        private static List<PropertyData> GetInformation(string scope, string query)
        {
            ManagementObjectSearcher searcher;
            int i = 0;
            List<PropertyData> arrayListInformationCollactor = new List<PropertyData>();
            try
            {
                query = $"SELECT * FROM {query}";
                if (scope == null)
                {
                    searcher = new ManagementObjectSearcher(query);
                }
                else
                {
                    searcher = new ManagementObjectSearcher(scope, query);
                }
                foreach (ManagementObject mo in searcher.Get())
                {
                    i++;
                    PropertyDataCollection searcherProperties = mo.Properties;
                    foreach (PropertyData searchProperties in searcherProperties)
                    {
                        arrayListInformationCollactor.Add(searchProperties);
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.GetAllMessages();
                Trace.WriteLine(exceptionMessage);
                //MessageBox.Show(exceptionMessage);
            }
            return arrayListInformationCollactor;
        }

        public static IEnumerable<WpfScreen.ScreenInfo> Screens { get { return WpfScreen.AllScreens; } }
        public static WpfScreen.ScreenInfo PrimaryScreen
        {
            get
            {
                var primaryScreen = WpfScreen.AllScreens.FirstOrDefault(screen => screen.IsPrimary);
                return primaryScreen;
            }
        }

        public static List<PropertyData> GetInformationRestricted(string scope, string query, string propertyName)
        {
            List<PropertyData> propertyData = GetInformation(scope, query);
            List<PropertyData> filteredPropertyData = propertyData.FindAll(x => x.Name == "Manufacturer");
            object o = filteredPropertyData[0].Value;
            return filteredPropertyData;
        }

        public static IEnumerable<EventLogEntry> GetEventLogErrors()
        {
            var logNames = new[] { "System", "Application" };
            var levels = new[] { EventLogEntryType.Error, EventLogEntryType.Warning };
            var events = new List<EventLogEntry>();

            foreach (var logName in logNames)
            {
                var log = new EventLog(logName);
                foreach (EventLogEntry entry in log.Entries)
                {
                    if (Array.Exists(levels, level => level == entry.EntryType))
                    {
                        events.Add(entry);
                    }
                }
            }

            return events.OrderByDescending(e => e.TimeGenerated).Take(100);
        }

        public static IEnumerable LocalUserAccounts
        {
            get
            {
                var path = "WinNT://" + Environment.MachineName;
                var users = new List<string>();
                var computer = new DirectoryEntry(path);

                foreach (DirectoryEntry child in computer.Children)
                {
                    if (child.SchemaClassName == "User")
                    {
                        users.Add(child.Name);
                    }
                }
                return users;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static IEnumerable<ManagementObject> BiosInfo
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                return searcher.Get().Cast<ManagementObject>();
            }
        }

        //TODO:
        //public static IEnumerable<string> GetSupportedResolutions()
        //{
        //    return WpfScreen.AllScreens.SelectMany(screen =>
        //        screen.GetSupportedResolutions()).Distinct();
        //}

        public static IEnumerable<ManagementObject> HotFixes
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_QuickFixEngineering");
                return searcher.Get().Cast<ManagementObject>();
            }
        }

        public static IEnumerable<ManagementObject> StartUpApplications
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_StartupCommand");
                return searcher.Get().Cast<ManagementObject>();
            }
        }
        public static IEnumerable<ManagementObject> ProcessorInfo
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                return searcher.Get().Cast<ManagementObject>();
            }
        }

        public static IEnumerable<X509Certificate2> InstalledCertificates
        {
            get
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                return store.Certificates.Cast<X509Certificate2>().Where(cert => cert.HasPrivateKey);
            }
        }

        public static IEnumerable<ManagementObject> LogonSessionInformation
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogonSession");
                return searcher.Get().Cast<ManagementObject>();
            }
        }

        public static IEnumerable<ManagementObject> LoggedInUsers
        {
            get
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                return searcher.Get().Cast<ManagementObject>();
            }
        }

        public static DateTime LocalTime => DateTime.Now;

        public static string NetFirewallRules
        {
            get
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "advfirewall firewall show rule name=all",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = startInfo };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            }
        }

        public static ServiceControllerStatus GetWinRMInformation()
        {
            using (ServiceController sc = new ServiceController("winrm"))
            {
                return sc.Status;
            }
        }

        static SysInfo()
        {
            GetSystemInfo(out systemInfo);
        }

        public static string GetValue(string query, string propertyName)
        {
            List<PropertyData> propertyData = GetInformationRestricted(query, propertyName);
            string value = null;
            if (propertyData.Count == 1)
            {
                PropertyData singlePropertyData = propertyData[0];
                if (singlePropertyData.Value != null)
                {
                    value = singlePropertyData.Value.ToString();
                }
            }
            return value;
        }

        //Todo: This could be moved to KnownFolders class as well. Makes more sense. So here only the propery would be needed to called.
        public static Dictionary<string, object> AllPropertyValues
        {
            get
            {
                PropertyInfo[] propertiesInfos = typeof(SysInfo).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                Dictionary<string, object> nameValueDictionary = new Dictionary<string, object>();
                foreach (PropertyInfo propertyInfo in propertiesInfos)
                {
                    object o = propertyInfo.GetValue(null);
                    nameValueDictionary.Add(propertyInfo.Name, o);
                }
                return nameValueDictionary;
            }
        }

        public static List<string> AllKnownFolders
        {
            get
            {
                List<string> knownFolders = new List<string>();
                foreach (KnownFolder knownFolder in Enum.GetValues(typeof(SystemInformation.SystemInformation.KnownFolder)))
                {
                    string knownFolderString = SystemInformation.SystemInformation.KnownFolders.GetPath(knownFolder);
                    knownFolders.Add(knownFolderString);
                }
                return knownFolders;
            }
        }

        /// <summary>
        /// Gets all of the physically labeled banks where the memory is located
        /// </summary>
        /// <returns>Bank labels</returns>
        public static List<object> BankLabels
        {
            get { return QueryWMI("SELECT BankLabel FROM Win32_PhysicalMemory", "BankLabel", 0); }
        }

        /// <summary>
        /// Gets the total capacity of the system's physical memory
        /// </summary>
        /// <returns>Total capacity of the physical memory</returns>
        public static ulong Capacity
        {
            get
            {
                ulong size = 0;
                List<object> capacities = QueryWMI("SELECT Capacity FROM Win32_PhysicalMemory", "Capacity", 0);
                foreach (ulong item in capacities)
                {
                    size += item;
                }
                size = size / 1024 / 1024;
                return size;
            }
        }

        public static ushort GetDataWidth(string bank)
        {
            return (ushort)QueryWMI("SELECT DataWidth FROM Win32_PhysicalMemory WHERE BankLabel = '{bank}'", "DataWidth");
        }

        public static string DecodeProductKey(byte[] digitalProductId)
        {
            // Offset of first byte of encoded product key in 
            //  'DigitalProductIdxxx" REG_BINARY value. Offset = 34H.
            const int keyStartIndex = 52;
            // Offset of last byte of encoded product key in 
            //  'DigitalProductIdxxx" REG_BINARY value. Offset = 43H.
            const int keyEndIndex = keyStartIndex + 15;
            // Possible alpha-numeric characters in product key.
            char[] digits = new char[]
            {
    'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R',
    'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9',
            };
            // Length of decoded product key
            const int decodeLength = 29;
            // Length of decoded product key in byte-form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;
            // Array of containing the decoded product key.
            char[] decodedChars = new char[decodeLength];
            // Extract byte 52 to 67 inclusive.
            ArrayList hexPid = new ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    // Do the actual decoding.
                    int digitMapIndex = 0;
                    for (int j = decodeStringLength - 1; j >= 0; j--)
                    {
                        int byteValue = digitMapIndex << 8 | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }

        public static List<CimInstance> QueryCIMInstances(string className, string computerName = "localhost")
        {
            string Namespace = @"root\cimv2";
            var instances = CimSession.Create(computerName)?.EnumerateInstances(Namespace, className)?.ToList();
            return instances;
        }

        private static object QueryWMI(string query, string property)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher(query).Get())
            {
                return managementObject;
            }
            return null;
        }

        private static List<object> QueryWMI(string query, string property, int numItems)
        {
            List<object> items = new List<object>();
            foreach (ManagementObject item in new ManagementObjectSearcher(query).Get())
            {
                items.Add(item[property]);
                if (numItems != 0 && items.Count == numItems)
                {
                    break;
                }
            }
            return items;
        }
    }
}