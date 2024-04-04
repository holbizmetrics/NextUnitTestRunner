using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.Diagnostics;

namespace NextUnit.TestExplorer
{
    public class MessageLogger : IMessageLogger
    {
        public void SendMessage(TestMessageLevel testMessageLevel, string message)
        {
            Trace.WriteLine($"{testMessageLevel}: {message}");
        }
    }
}
