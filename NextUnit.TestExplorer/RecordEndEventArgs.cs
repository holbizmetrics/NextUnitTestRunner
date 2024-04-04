using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class RecordEndEventArgs : RecordEventArgs
    {
        public TestOutcome TestOutcome { get; set; }
        public RecordEndEventArgs(TestCase testCase, TestOutcome outCome)
        {
            this.TestCase = testCase;
            this.TestOutcome = outCome;
        }
    }
}
