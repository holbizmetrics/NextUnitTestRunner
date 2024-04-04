using System.Diagnostics;

namespace NextUnit.Benchmarking.Measurements
{
    /// <summary>
    /// Gets the handles in use.
    /// </summary>
    public class HandleCountMeasurement : IBenchmarkAction
    {
        private int _before;
        private int _after;

        public string Unit { get; } = "count";
        public void Start() => _before = Process.GetCurrentProcess().HandleCount;
        public void Stop() => _after = Process.GetCurrentProcess().HandleCount;
        public BenchmarkResult GetResult()
        {
            int result = _after - _before;
            return new BenchmarkResult($"Handle Count Difference: {result}", result, Unit);
        }
    }
}