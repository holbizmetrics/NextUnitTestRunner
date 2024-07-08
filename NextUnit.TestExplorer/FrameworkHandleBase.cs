using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

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
}
