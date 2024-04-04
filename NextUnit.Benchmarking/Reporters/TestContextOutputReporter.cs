using NextUnit.Core;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Outputs a text to the current TestContext.
    /// </summary>
    public class TestContextOutputReporter : IBenchmarkReporter
    {
        public void Report(string message)
        {
            //NextUnitTestExecutionContext.Out.WriteLine(message);
        }
    }
}
