using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class FrameworkHandle : FrameworkHandleBase, IFrameworkHandle
    {
        public bool EnableShutdownAfterTestRun { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TestResult CurrentResult { get; private set; } = null;
        public TestCase CurrentTestCase { get; private set; } = null;
   
        public int LaunchProcessWithDebuggerAttached(string filePath, string? workingDirectory, string? arguments, IDictionary<string, string?>? environmentVariables)
        {
            throw new NotImplementedException();
        }

        public void RecordAttachments(IList<AttachmentSet> attachmentSets)
        {
            throw new NotImplementedException();
        }

        public void RecordEnd(TestCase testCase, TestOutcome outcome)
        {
            OnTestRecordEnd(new RecordEndEventArgs(testCase, outcome));
        }

        public void RecordResult(TestResult testResult)
        {
            OnTestResultRecording(new RecordResultEventArgs(testResult));
        }

        public void RecordStart(TestCase testCase)
        {
            OnTestRecordStart(new RecordStartEventArgs(testCase));
        }

        public void SendMessage(TestMessageLevel testMessageLevel, string message)
        {
            OnMessage(new DataCollectionMessageEventArgs(testMessageLevel, message));
        }
    }
}
