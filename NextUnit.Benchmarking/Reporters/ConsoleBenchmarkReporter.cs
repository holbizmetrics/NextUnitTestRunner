using System;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Outputs a given text to the console.
    /// </summary>
    public class ConsoleBenchmarkReporter : IBenchmarkReporter
    {
        public void Report(string message) => Console.WriteLine(message);
    }
}
