using NextUnitTestResult = NextUnit.TestRunner.TestResult;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NextUnit.TestRunner;

namespace NextUnit.TestAdapter
{
    public static class TestAdapterHelper
    { /// <summary>
      /// Converts a NextUnit TestResult to a Microsoft TestResult. As needed by the TestAdapter to be correctly shown, e.g. in the Test Explorer.
      /// </summary>
      /// <param name="testCase"></param>
      /// <param name="nextUnitTestResult"></param>
      /// <returns></returns>
        public static Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult ConvertTestCase(this TestCase testCase, NextUnitTestResult nextUnitTestResult)
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
