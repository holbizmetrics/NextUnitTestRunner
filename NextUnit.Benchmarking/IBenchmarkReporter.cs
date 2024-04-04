namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Interface to report results.
    /// </summary>
    public interface IBenchmarkReporter
    {
        void Report(string message);
    }
}
