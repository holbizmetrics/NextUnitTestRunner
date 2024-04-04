using System.Diagnostics;

namespace NextUnit.Benchmarking.Measurements
{
    /// <summary>
    /// Measures the time.
    /// </summary>
    public class TimeMeasurement : IBenchmarkAction
    {
        private Stopwatch _Stopwatch = new Stopwatch();
        public string Unit { get; } = "ms";

        public void Start() => _Stopwatch.Start();

        public void Stop() => _Stopwatch.Stop();

        public BenchmarkResult GetResult()
        {
            long result = _Stopwatch.ElapsedMilliseconds;
            return new BenchmarkResult($"Time Elapsed: {result} ms", result, Unit);
        }
    }
}