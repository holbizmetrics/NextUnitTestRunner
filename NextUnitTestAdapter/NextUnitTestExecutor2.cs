#define ADAPTER_TEST

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Diagnostics;

namespace NextUnit.TestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    public class NextUnitTestExecutor2 : NextUnitBaseExecutor, ITestExecutor2
    {
        public void Cancel()
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
        }

        /// <summary>
        /// This method is responsible to run the Test(s) selected in Visual Studio's Test Explorer. 
        /// </summary>
        /// <param name="tests"></param>
        /// <param name="runContext"></param>
        /// <param name="frameworkHandle"></param>
        public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
            frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);
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
                catch(InvalidOperationException ex)
                {
                    frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, ex.ToString());
                    frameworkHandle.RecordEnd(test, TestOutcome.Failed);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions during test execution
                    frameworkHandle.RecordEnd(test, TestOutcome.Failed);
                    //frameworkHandle.RecordException(ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="runContext"></param>
        /// <param name="frameworkHandle"></param>
        public void RunTests(IEnumerable<string>? sources, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="runContext"></param>
        /// <returns></returns>
        public bool ShouldAttachToTestHost(IEnumerable<string>? sources, IRunContext runContext)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tests"></param>
        /// <param name="runContext"></param>
        /// <returns></returns>
        public bool ShouldAttachToTestHost(IEnumerable<TestCase>? tests, IRunContext runContext)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
            return false;
        }
    }
}
