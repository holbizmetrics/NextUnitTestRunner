using System;

namespace NextUnit.Benchmarking.Measurements
{
    /// <summary>
    /// Gets the memory by leveraging the GC.
    /// </summary>
    public class MemoryUsageMeasurement : IBenchmarkAction
    {
        private long _before;
        private long _after;

        public string Unit { get; } = "bytes";

        public void Start() => _before = GC.GetTotalMemory(forceFullCollection: true);

        public void Stop() => _after = GC.GetTotalMemory(forceFullCollection: true);

        public BenchmarkResult GetResult()
        {
            double result = _after - _before;
            return new BenchmarkResult($"Memory used: {result} bytes", Unit);
        }
    }
}
