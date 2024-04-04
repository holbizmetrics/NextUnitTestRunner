using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorerUI
{
    public class TestMethod
    {
        public string Name { get; set; }
        public TestCase TestCase { get; set; }
        public TestOutcome Outcome { get; internal set; }
    }
}
