#define ADAPTER_TEST

#if ADAPTER_TEST
using System.Diagnostics;
#endif
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnitTestAdapter;

namespace NextUnit.TestAdapter
{
    /// <summary>
    /// 
    /// </summary>
    [ExtensionUri(Definitions.DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    public class NextUnitTestExecutor : NextUnitBaseExecutor, ITestExecutor
    {
        /// <summary>
        /// 
        /// </summary>
        public void Cancel()
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tests"></param>
        /// <param name="runContext"></param>
        /// <param name="frameworkHandle"></param>
        public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
#if ADAPTER_TEST
            Debug.WriteLine($"Hello from {nameof(NextUnitTestExecutor)}");
            Debugger.Launch();
            Debugger.Break();
#endif
            var settings = runContext?.RunSettings?.SettingsXml;
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Informational, "RunTests");
            foreach (TestCase test in tests)
            {
                // Example: Mark the start of the test
                frameworkHandle.RecordStart(test);

                try
                {
                    // Execute the test and get the result
                    Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult result = ExecuteTest(test);

                    // Example: Record the outcome of the test
                    frameworkHandle.RecordResult(result);
                    frameworkHandle.RecordEnd(test, TestOutcome.Passed);
                }
                catch (FileLoadException ex)
                {

                }
                catch(TargetParameterCountException ex)
                {

                }
                catch (Exception ex)
                {
                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var fileName = frame.GetFileName();
                    var lineNumber = frame.GetFileLineNumber();
                    
                    test.LineNumber = lineNumber;
                    test.CodeFilePath = fileName;
                    // Handle any exceptions during test execution
                    frameworkHandle.RecordEnd(test, TestOutcome.Failed);
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
            var settings = runContext.RunSettings.SettingsXml;
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, "RunTests");
        }
    }
}
