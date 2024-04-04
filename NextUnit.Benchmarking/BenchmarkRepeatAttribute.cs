using NextUnit.Benchmarking.Measurements;
using System.Diagnostics;
using System.Text;
using NextUnit.Core;
using NextUnit.Core.TestAttributes;
using NextUnit.TestEnvironment;

namespace NextUnit.Benchmarking
{
    // TODO: This attribute implementation demands that the hook context points work correctly.
    /// <summary>
    /// Use this to get several results of a Benchmark
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BenchmarkRepeatAttribute : RetryAttribute, ITestContext
    {
        private BenchmarkController BenchmarkController { get; } = new BenchmarkController();
        private readonly int _count;

        public BenchmarkRepeatAttribute(int count)
            : base(count)
        {
            _count = count;
        }

        public void BeforeTestExecution()
        {
            throw new NotImplementedException();
        }

        public void AfterTestExecution()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Will be used by the BenchmarkRepeatAttribute to wrap the command for NUnit.
    /// </summary>
    public class RepeatedTestCommand
    {
        private IEnumerable<IBenchmarkAction> BenchmarkActions { get; set; } = null;
        private ReportController ReportController { get; } = new ReportController();

        public BenchmarkController BenchmarkController { get; set; } = new BenchmarkController();
        private readonly int _count;

        public RepeatedTestCommand(int count)
        {
            _count = count;
        }

        private void Initialize()
        {
            BenchmarkActions = new List<IBenchmarkAction>() {
                    new CPUUsageMeasurement(),
                    //new EFQueryMeasurement(),
                    new GCCollectionMeasurement(),
                    new HandleCountMeasurement(),
                    new MemoryUsageMeasurement(),
                    new TimeMeasurement()
                };

            foreach (IBenchmarkAction action in BenchmarkActions)
            {
                this.BenchmarkController.AddAction(action);
            }
        }

        /// <summary>
        /// Executes the whole mechanism.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public TestResult Execute(TestExecutionContext context)
        {
            OutputWarnings();
            Initialize();

            //ExecuteTestNTimes(context);

            // After all iterations, perform statistical analysis
            if (BenchmarkController.Results.Count > 0)
            {
                // Attach the calculated statists

                // Generate a detailed report based on the aggregated results
                string report = GenerateReportFromResults(BenchmarkController.Results);

                // Use ReportController to distribute the report through all configured reporters
                ReportController.AddReporter(new TestContextOutputReporter());
                ReportController.ReportAll(report);
            }

            // we have to set the state here explicitly. Otherwise this won't be shown as being executed.
            context.CurrentResult.State = ExecutionState.Passed;
            return context.CurrentResult;
        }

        /// <summary>
        /// Execute the test n times (n is here concretely count and was provided by the constructor
        /// </summary>
        /// <param name="context"></param>
        protected virtual void ExecuteTestNTimes(TestExecutionContext context)
        {
            for (int i = 0; i < _count; i++)
            {
                BenchmarkController.StartAll();
                // execute the inner command, which runs the test
                //context.CurrentResult = innerCommand.Execute(context);
                BenchmarkController.StopAll();

                // Collect the benchmark results for this iteration
                // This assumes your benchmarking system can expose the latest result
                if (context.CurrentResult.State != ExecutionState.Passed)
                {
                    // if the test fails, don't continue repeating
                    context.CurrentResult.State = ExecutionState.Failed;
                    break;
                }
            }
        }

        private void OutputWarnings()
        {
            // output warning if we run with a debugger attached.
            if (Debugger.IsAttached)
            {
                ReportController.ReportAll("Warning: Please be aware that an attached debugger can distort the results.");
            }
            // output warning if we run in debug mode.
            if (BenchmarkHelper.IsDebugMode)
            {
                ReportController.ReportAll("Warning: Please be aware that debug mode can distort the results.");
            }
        }

        /// <summary>
        /// Generates a report from the results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private string GenerateReportFromResults(BenchmarkResultsHistory results)
        {
            // This method would format the results into a detailed report string
            // Example:

            StringBuilder stringBuilder = new StringBuilder();
            foreach (IBenchmarkAction action in BenchmarkActions)
            {
                Type actionType = action.GetType();

                string typeName = actionType.Name;

                double mean = results.Mean(actionType);
                double median = results.Median(actionType);
                double skew = results.Skewness(actionType);
                double kurtosis = results.Kurtosis(actionType);
                double min = results.Min(actionType);
                double max = results.Max(actionType);
                double stdError = results.StandardError(actionType);
                double stdDeviation = results.StandardDeviation(actionType);

                string result =
$@"
-----------------------------------------
Benchmark Report: {typeName}
-----------------------------------------

Min: {min}
Max: {max}
Mean: {mean}
Median: {median}
Skew: {skew}
Kurtosis: {kurtosis}
Std Dev: {stdDeviation}
Std Err: {stdError}

";
                stringBuilder.Append(result);
            }
            return stringBuilder.ToString();
        }
    }
}

