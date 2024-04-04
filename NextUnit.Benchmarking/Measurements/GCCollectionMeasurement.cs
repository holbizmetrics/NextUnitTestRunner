using System;
using System.Text;

namespace NextUnit.Benchmarking.Measurements
{
    /// <summary>
    /// Get the max generations before and after measurement.
    /// </summary>
    public class GCCollectionMeasurement : IBenchmarkAction
    {
        private int[] _before = new int[GC.MaxGeneration + 1];
        private int[] _after = new int[GC.MaxGeneration + 1];

        public string Unit { get; } = "count";

        public void Start()
        {
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                _before[i] = GC.CollectionCount(i);
            }
        }

        public void Stop()
        {
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                _after[i] = GC.CollectionCount(i);
            }
        }

        public BenchmarkResult GetResult()
        {
            var reports = new StringBuilder();
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                int result = _after[i] - _before[i];
                reports.AppendLine($"Gen {i} Collections: {result}");
            }
            return new BenchmarkResult(reports.ToString(), 0, Unit);
        }
    }
}
