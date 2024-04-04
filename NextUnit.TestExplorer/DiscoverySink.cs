using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class DiscoverySink : ITestCaseDiscoverySink
    {
        public List<TestCase> testCases = new List<TestCase>();
        public void SendTestCase(TestCase discoveredTest)
        {
            testCases.Add(discoveredTest);
        }
    }
}
