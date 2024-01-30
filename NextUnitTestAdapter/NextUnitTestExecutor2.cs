using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Diagnostics;

namespace NextUnit.TestAdapter
{
    public class NextUnitTestExecutor2 : NextUnitBaseExecutor, ITestExecutor2
    {
        public void Cancel()
        {
            Debugger.Launch();
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            Debugger.Launch();
            foreach (var test in tests)
            {
                // Example: Mark the start of the test
                frameworkHandle.RecordStart(test);

                try
                {
                    // Execute the test and get the result
                    var result = ExecuteTest(test);

                    // Example: Record the outcome of the test
                    frameworkHandle.RecordResult(result);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions during test execution
                    frameworkHandle.RecordEnd(test, TestOutcome.Failed);
                    //frameworkHandle.RecordException(ex);
                }
            }
        }

        public void RunTests(IEnumerable<string>? sources, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            Debugger.Launch();
            throw new NotImplementedException();
        }

        public bool ShouldAttachToTestHost(IEnumerable<string>? sources, IRunContext runContext)
        {
            Debugger.Launch();
            throw new NotImplementedException();
        }

        public bool ShouldAttachToTestHost(IEnumerable<TestCase>? tests, IRunContext runContext)
        {
            Debugger.Launch();
            throw new NotImplementedException();
        }
    }
}
