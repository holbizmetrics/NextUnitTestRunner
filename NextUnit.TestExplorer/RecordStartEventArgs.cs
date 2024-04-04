using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class RecordStartEventArgs : RecordEventArgs
    {
        public RecordStartEventArgs(TestCase testCase)
        {
            this.TestCase = testCase;
        }
    }
}
