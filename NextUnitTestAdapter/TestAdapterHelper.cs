using NextUnitTestResult = NextUnit.TestRunner.TestResult;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NextUnit.TestRunner;

namespace NextUnit.TestAdapter
{
    public static class TestAdapterHelper
    { /// <summary>
      /// 
      /// </summary>
      /// <param name="testCase"></param>
      /// <param name="nextUnitTestResult"></param>
      /// <returns></returns>
        public static Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult ConvertTestCase(this TestCase testCase, NextUnitTestResult nextUnitTestResult)
        {
            Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult testResult = new Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult(testCase);
            TestOutcome testOutcome = nextUnitTestResult.State switch
            {
                ExecutedState.Passed => testResult.Outcome = TestOutcome.Passed,
                ExecutedState.Failed => testResult.Outcome = TestOutcome.Failed,
                ExecutedState.Skipped => testResult.Outcome = TestOutcome.Skipped,
                ExecutedState.NotFound => testResult.Outcome = TestOutcome.NotFound,
            };
            testResult.Outcome = testOutcome;
            return testResult;
        }
    }
}
