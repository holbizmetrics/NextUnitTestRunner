using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.ComponentModel;
using System.Diagnostics;

namespace NextUnitTestAdapter
{
    [ExtensionUri("executor://NextUnitTestDiscoverer")]
    [DefaultExecutorUri("executor://NextUnitTestDiscoverer")]
    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [Category("managed")]
    public class NextUnitTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            Debugger.Launch();
            logger.SendMessage(TestMessageLevel.Error, "DiscoverTests");
        }
    }
}
