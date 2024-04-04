using NextUnit.Benchmarking;
using System;
using System.Diagnostics;

namespace NextUnit.Benchmarking.Measurements
{
    /// <summary>
    /// Gets hold of the current CPU measurements
    /// </summary>
    public class CPUUsageMeasurement : IBenchmarkAction
    {
        private TimeSpan _startCPUTime;
        private TimeSpan _endCPUTime;
        private readonly Process _currentProcessToMeasure;
        public string Unit => "ms";
        public CPUUsageMeasurement()
            : this(null)
        {
        }

        public CPUUsageMeasurement(Process process = null)
        {
            _currentProcessToMeasure = process == null ? Process.GetCurrentProcess() : process;
        }

        /// <summary>
        /// Starts the time measuring by sampling TotalProcessorTime.
        /// </summary>
        public void Start()
        {
            _currentProcessToMeasure.Refresh();
            _startCPUTime = _currentProcessToMeasure.TotalProcessorTime;
        }

        /// <summary>
        /// Ends the time measuring by sampling TotalProcessorTime.
        /// </summary>
        public void Stop()
        {
            _currentProcessToMeasure.Refresh();
            _endCPUTime = _currentProcessToMeasure.TotalProcessorTime;
        }

        /// <summary>
        /// Report the used time.
        /// </summary>
        /// <returns></returns>
        public BenchmarkResult GetResult()
        {
            var cpuUsed = _endCPUTime - _startCPUTime;
            return new BenchmarkResult($"CPU Time Used: {cpuUsed.TotalMilliseconds}", cpuUsed, Unit);
        }
    }
}
