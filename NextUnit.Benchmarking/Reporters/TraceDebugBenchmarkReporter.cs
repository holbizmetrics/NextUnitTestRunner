using System.Diagnostics;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Please be aware that this one is dependent on the listeners added to trace.Listeners.
    /// </summary>
    public class TraceDebugBenchmarkReporter : IBenchmarkReporter
    {
        public void Report(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
