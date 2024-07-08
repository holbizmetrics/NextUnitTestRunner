using NextUnit.Core.Extensions;
using System.Diagnostics;

namespace NexUnit.TestDataGenerator
{
    public class ColoredConsoleTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            // Use your extension method to write the message to the console without a newline
            message.WriteColoredLine();
        }

        public override void WriteLine(string message)
        {
            // Use your extension method to write the message to the console with a newline
            message.WriteColoredLine();
        }
    }
}
