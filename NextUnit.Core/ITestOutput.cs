using NextUnit.Core;

namespace NextUnit.Core.Output
{
    public interface ITestOutput
    {
        void LogMessage(string message);
        void LogError(string message);
        void ReportResult(TestResult result);
    }
}
