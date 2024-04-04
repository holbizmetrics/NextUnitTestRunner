using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnit.Core;
using NextUnit.Core.Output;
using NextUnitTestResult = NextUnit.Core.TestResult;

namespace NextUnit.TestRunner.Output
{
    /// <summary>
    /// This will be used for outputs to test explorer.
    /// </summary>
    public class TestContextOutput : ITestOutput
    {
        IFrameworkHandle handle;
        public void LogError(string message)
        {
            handle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, message);
        }

        public void LogMessage(string message)
        {
            handle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Informational, message);
        }

        public void ReportResult(NextUnitTestResult result)
        {
            handle.RecordResult(Extension.ConvertTestResult(result, null));
        }
    }

    public static class Extension
    {
        //TODO: see inline XML comment.
        /// <summary>
        /// This is a duplicate from TestAdapterHelper and definitely either TestAdapterHelper has to be used in another way
        /// or its methods have to put somewhere else maybe into the core?
        /// </summary>
        /// <param name="nextUnitTestResult"></param>
        /// <param name="testCase"></param>
        /// <returns></returns>
        public static Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult ConvertTestResult(this NextUnitTestResult nextUnitTestResult, TestCase testCase)
        {
            Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult testResult = new Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult(testCase);
            TestOutcome testOutcome = nextUnitTestResult.State switch
            {
                ExecutionState.Passed => testResult.Outcome = TestOutcome.Passed,
                ExecutionState.Failed => testResult.Outcome = TestOutcome.Failed,
                ExecutionState.Skipped => testResult.Outcome = TestOutcome.Skipped,
                ExecutionState.NotFound => testResult.Outcome = TestOutcome.NotFound,
            };
            testResult.Outcome = testOutcome;
            return testResult;
        }

    }
}
