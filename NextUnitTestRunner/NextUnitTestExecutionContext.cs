using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnitTestRunner
{
    /// <summary>
    /// This is valid for the whole test suite
    /// (so here the time stamps relate to where the first test started,
    /// and the last test executed.
    /// Thus, also the TestRunTime corresponds to the whole test run.
    /// )
    /// </summary>
    public class NextUnitTestExecutionContext
    {
        public static DateTime TestRunStart { get; internal set; }
        public static DateTime TestRunEnd { get; internal set; }
        public static TimeSpan TestRunTime { get { return TestRunEnd - TestRunStart; } }
        public static List<TestResult> TestResults { get; } = new List<TestResult>();

        public static string ToString()
        {
            return
$@"TestRunStart: {TestRunStart};
TestRunEnd: {TestRunEnd}
TestRunTime: {TestRunTime}";
        }
    }
}
