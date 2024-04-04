using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Diagnostics;

namespace NextUnit.TestExplorer
{
    public class FrameworkHandleBase
    {
        public delegate void TestRecordResultHandler(object sender, RecordResultEventArgs e);
        public event TestRecordResultHandler TestResultRecording;

        public delegate void RecordStartHandler(object sender, RecordStartEventArgs e);
        public event RecordStartHandler TestCaseRecordStarting;

        public delegate void RecordEndHandler(object sender, RecordEndEventArgs e);
        public event RecordEndHandler TestCaseRecordEnding;

        public delegate void MessageEventHandler(object sender, DataCollectionMessageEventArgs e);
        public event MessageEventHandler MessageHandler;
        protected virtual void OnTestResultRecording(RecordResultEventArgs e)
        {
            TestResultRecording?.Invoke(this, e);
        }

        protected virtual void OnTestRecordStart(RecordStartEventArgs e)
        {
            TestCaseRecordStarting?.Invoke(this, e);
        }

        protected virtual void OnTestRecordEnd(RecordEndEventArgs e)
        {
            TestCaseRecordEnding?.Invoke(this, e);
        }

        protected virtual void OnMessage(DataCollectionMessageEventArgs e)
        {
            MessageHandler?.Invoke(this, e);
        }
    }

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
