#define ADAPTER_TEST
using NextUnitTestResult = NextUnit.Core.TestResult;

#if ADAPTER_TEST
#endif

using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    public class NextUnitTestExecutor2 : NextUnitBaseExecutor, ITestExecutor2
    {
        public void Cancel()
        {
#if ADAPTER_TEST
            Debug.WriteLine($"Hello from {nameof(NextUnitTestExecutor2)}");
            Debugger.Launch();
            Debugger.Break();
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
            Debug.WriteLine($"Hello from {nameof(NextUnitTestExecutor2)}");
            Debugger.Launch();
            Debugger.Break();
#endif
            Tests(tests, runContext, frameworkHandle);
            //frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Informational, "RunTests");
        }

        private void Tests(IEnumerable<string> sources, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            //Tests(tests, runContext, frameworkHandle);
        }

        private void Tests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            foreach (var test in tests)
            {
                // Example: Mark the start of the test
                frameworkHandle?.RecordStart(test);

                // Execute the test and get the result
                NextUnitTestResult result = ExecuteTest(test);
                string stackTrace = result.StackTrace;
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    frameworkHandle?.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, stackTrace);
                }

                TestResult testResult = test.ConvertTestCase(result);

                // Example: Record the outcome of the test
                frameworkHandle?.RecordResult(testResult);
                frameworkHandle?.RecordEnd(test, testResult.Outcome);
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
            if (runContext.IsBeingDebugged && frameworkHandle is IFrameworkHandle2 frameworkHandle2)
            {
                Process process = Process.GetCurrentProcess();

                frameworkHandle2.AttachDebuggerToProcess(process.Id);
            }

            Tests(sources, runContext, frameworkHandle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="runContext"></param>
        /// <returns></returns>
        public bool ShouldAttachToTestHost(IEnumerable<string>? sources, IRunContext runContext)
        {
//#if ADAPTER_TEST
            Debugger.Launch();
//#endif
            Tests(sources, runContext, null);
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
            Tests(tests, runContext, null);
            return false;
        }
    }
}
