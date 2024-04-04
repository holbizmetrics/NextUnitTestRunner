namespace NextUnit.Benchmarking.Measurements
{
    public class EFQueryMeasurement : IBenchmarkAction
    {
        public string Unit { get; } = string.Empty;
        public void Start()
        {
        }

        public void Stop()
        {
        }

        public BenchmarkResult GetResult()
        {
            return new BenchmarkResult("EF measurement is not implemented, yet.", 0, Unit);
        }
    }
}
