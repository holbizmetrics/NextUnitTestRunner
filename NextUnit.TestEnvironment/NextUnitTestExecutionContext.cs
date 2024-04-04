using NextUnit.Core;
using NextUnit.Core.Output;
using System.Reflection;

namespace NextUnit.TestEnvironment
{
    //TODO: Check for set time. This should never be negative. But sometimes it is.
    /// <summary>
    /// This is valid for the whole test suite
    /// (so here the time stamps relate to where the first test started,
    /// and the last test executed.
    /// Thus, also the TestRunTime corresponds to the whole test run.
    /// )
    /// </summary>
    public static class NextUnitTestExecutionContext
    {
        private static DateTime CurrentDateTime = DateTime.Now;
        public static DateTime TestRunStart { get; set; } = CurrentDateTime;
        public static DateTime TestRunEnd { get; set; } = CurrentDateTime;
        public static TimeSpan TestRunTime { get { return TestRunEnd - TestRunStart; } }
        public static List<TestResult> TestResults { get; } = new List<TestResult>();

        public static KeyValuePair<TestResult, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> CurrentTest { get; set; }

        public static string ToString()
        {
            return
$@"TestRunStart: {TestRunStart};
TestRunEnd: {TestRunEnd}
TestRunTime: {TestRunTime}";
        }
    }

    public class TestExecutionContext
    {
        public TestResult TestResult { get; set; } = new TestResult();
        public NextUnitTestEnvironmentContext EnvironmentContext { get; set; }

        public static ITestOutput TestOutput { get; set; } = null;
        public TestResult CurrentResult { get; set; }

        public TestExecutionContext()
        {
            EnvironmentContext = new NextUnitTestEnvironmentContext();
        }
    }
}
