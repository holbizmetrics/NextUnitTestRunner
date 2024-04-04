using NextUnit.Core.Extensions;
using NextUnit.HardwareContext.SystemInformation;
using System.Globalization;
using System.Management;

namespace NextUnit.TestEnvironment
{
    /// <summary>
    /// Same as TestContext in other frameworks.
    /// </summary>
    public class NextUnitTestEnvironmentContext
    {
        public static string MachineName { get; } = SysInfo.MachineName;
        public static string CommandLine { get; } = SysInfo.CommandLine;
        public static int ProcessorCount { get; } = SysInfo.ProcessorCount;
        //public static string ProcessorName { get; } = SysInfo.ProcessorInfo;
        public static IEnumerable<ManagementObject> BiosInfo {get;} = SysInfo.BiosInfo;
        public static ulong Capacity { get; } = SysInfo.Capacity;
        public static OperatingSystem OperatingSystem { get; } = SysInfo.OperatingSystem;
        public CultureInfo CurrentCulture { get; } = Thread.CurrentThread.CurrentCulture;
        public CultureInfo CurrentUICulture { get; } = Thread.CurrentThread.CurrentUICulture;
        public static string ToString()
        {
            return
$@"MachineName: {MachineName}
CommandLine: {CommandLine}
ProcessorCount: {ProcessorCount}
BiosInfo: {BiosInfo.As<string>()}
Capacity: {Capacity}
OperatingSystem: {OperatingSystem}";
        }
    }
}
