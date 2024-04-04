using System.Diagnostics;

namespace NextUnit.Core
{
    /// <summary>
    /// Provides a logger to output messages for tests.
    /// Can be used in a test.
    /// </summary>
    public class TestMessageSink : TraceListener
    {
        public override void Write(string? message)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine(string? message)
        {
            throw new NotImplementedException();
        }
    }
}
