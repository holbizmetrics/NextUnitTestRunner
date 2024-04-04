namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Interface for the benchmark action.
    /// </summary>
    public interface IBenchmarkAction
    {
        void Start();
        void Stop();
        BenchmarkResult GetResult();
        public string Unit { get; }
    }
}
