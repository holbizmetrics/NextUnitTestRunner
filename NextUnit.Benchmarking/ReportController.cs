using System.Collections.Generic;
using System.Linq;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Report controller steers the output thats given to the directed dedicated outputs.
    /// </summary>
    public class ReportController
    {
        private readonly List<IBenchmarkReporter> _benchmarkReporters = new List<IBenchmarkReporter>();

        public ReportController()
        {
        }

        public ReportController(params IBenchmarkReporter[] benchmarkReporters)
        {
            AddReporters(benchmarkReporters);
        }

        public ReportController WithReporters(params IBenchmarkReporter[] benchmarkReporters)
        {
            AddReporters(benchmarkReporters);
            return this;
        }

        public void AddReporters(params IBenchmarkReporter[] benchmarkReporters)
        {
            foreach (var reporter in benchmarkReporters)
            {
                this.AddReporter(reporter);
            }
        }

        /// <summary>
        /// Add an aoutput.
        /// </summary>
        /// <param name="benchmarkReporter"></param>
        public void AddReporter(IBenchmarkReporter benchmarkReporter)
        {
            // do not allow to add the same reporter (of same type) twice.
            if (!_benchmarkReporters.Any(a => a.GetType() == benchmarkReporter.GetType()))
            {
                _benchmarkReporters.Add(benchmarkReporter);
            }
        }

        /// <summary>
        /// Report to all given outputs.
        /// </summary>
        /// <param name="report"></param>
        public void ReportAll(string report)
        {
            foreach (var reporter in _benchmarkReporters)
            {
                reporter.Report(report);
            }
        }
    }
}
