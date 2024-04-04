using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class RecordResultEventArgs : EventArgs
    {
        public TestResult TestResult { get; set; }
        public RecordResultEventArgs(TestResult testResult)
        {
            this.TestResult = testResult;
        }
    }
}
