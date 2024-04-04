using System.Diagnostics;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Outputs a given text to the debug output.
    /// </summary>
    public class DebugConsoleBenchmarkReporter : IBenchmarkReporter
    {
        public void Report(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
