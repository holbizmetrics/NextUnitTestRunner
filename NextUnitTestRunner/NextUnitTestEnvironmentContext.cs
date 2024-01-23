using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NextUnitHardwareContext.SystemInformation;

namespace NextUnitTestRunner
{
    /// <summary>
    /// Same as TestContext in other frameworks.
    /// </summary>
    public static class NextUnitTestEnvironmentContext
    {
        public static string MachineName { get; } = SysInfo.MachineName;
        public static string CommandLine { get; } = SysInfo.CommandLine;
        public static int ProcessorCount { get; } = SysInfo.ProcessorCount;
        //public static string ProcessorName { get; } = SysInfo.ProcessorInfo;
        public static object BiosInfo {get;} = SysInfo.BiosInfo;
        public static object Capacity { get; } = SysInfo.Capacity;
        public static OperatingSystem OperatingSystem { get; } = SysInfo.OperatingSystem;
        public static CultureInfo CurrentCulture { get; } = Thread.CurrentThread.CurrentCulture;
        public static CultureInfo CurrentUICulture { get; } = Thread.CurrentThread.CurrentUICulture;
        public static string ToString()
        {
            return
$@"MachineName: {MachineName}
CommandLine: {CommandLine}
ProcessorCount: {ProcessorCount}
BiosInfo: {BiosInfo}
Capacity: {Capacity}
OperatingSystem: {OperatingSystem}";
        }
    }
}
