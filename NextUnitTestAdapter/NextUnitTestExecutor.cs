#define ADAPTER_TEST

#if ADAPTER_TEST
using System.Diagnostics;
#endif
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnitTestResult = NextUnit.Core.TestResult;
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
            Debugger.Break();
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

            Exception lastException = null;
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Informational, "RunTests");
            foreach (TestCase test in tests)
            {
                // Example: Mark the start of the test
                frameworkHandle.RecordStart(test);

                try
                {
                    // Execute the test and get the result
                    NextUnitTestResult nextUnitTestResult = ExecuteTest(test);

                    TestResult result = test.ConvertTestCase(nextUnitTestResult);

                    // Example: Record the outcome of the test
                    frameworkHandle.RecordResult(result);
                    frameworkHandle.RecordEnd(test, TestOutcome.Passed);
                }
                catch (FileLoadException ex)
                {
                    lastException = ex;
                }
                catch(TargetParameterCountException ex)
                {
                    lastException = ex;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var fileName = frame.GetFileName();
                    var lineNumber = frame.GetFileLineNumber();
                    
                    test.LineNumber = lineNumber;
                    test.CodeFilePath = fileName;
                    // Handle any exceptions during test execution
                    frameworkHandle.RecordEnd(test, TestOutcome.Failed);
                }
                finally
                {
                    if (lastException != null)
                    {
                        frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, lastException.ToString());
                        frameworkHandle.RecordEnd(test, TestOutcome.Failed);
                    }
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
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Informational, "RunTests");
        }
    }
}
