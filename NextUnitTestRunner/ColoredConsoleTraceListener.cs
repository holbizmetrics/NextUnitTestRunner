using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextUnit.TestRunner.Extensions;
namespace NexUnit.TestDataGenerator
{
    public class ColoredConsoleTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            // Use your extension method to write the message to the console without a newline
            ConsoleExtensions.WriteColoredLine(message);
        }

        public override void WriteLine(string message)
        {
            // Use your extension method to write the message to the console with a newline
            ConsoleExtensions.WriteColoredLine(message);
        }
    }
}
