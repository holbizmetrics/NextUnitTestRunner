using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace NextUnitTestAdapter
{
    [ExtensionUri("executor://NextUnitTestDiscoverer")]
    public class NextUnitTestExecutor : ITestExecutor
    {
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            var settings = runContext.RunSettings.SettingsXml;
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, "RunTests");
        }

        public void RunTests(IEnumerable<string>? sources, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            var settings = runContext.RunSettings.SettingsXml;
            frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, "RunTests");
        }
    }

    public class NextUnitTestExecutor2 : ITestExecutor2
    {
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<string>? sources, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
        {
            throw new NotImplementedException();
        }

        public bool ShouldAttachToTestHost(IEnumerable<string>? sources, IRunContext runContext)
        {
            throw new NotImplementedException();
        }

        public bool ShouldAttachToTestHost(IEnumerable<TestCase>? tests, IRunContext runContext)
        {
            throw new NotImplementedException();
        }
    }
}
