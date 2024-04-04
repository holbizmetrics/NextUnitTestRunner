using NextUnit.Core;
using NextUnit.Core.Output;

namespace NextUnit.TestRunner.Output
{
    public class ConsoleTestOutput : ITestOutput
    {
        public void LogError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ReportResult(TestResult result)
        {
            Console.WriteLine($"Test {result.DisplayName}{result.State}");
        }
    }
}
