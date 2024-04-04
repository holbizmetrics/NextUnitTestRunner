using System;
using System.Collections.Generic;
using System.Linq;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBenchmarkController
    {

    }

    /// <summary>
    /// Use this to start the benchmarks and also report them to the set reporting outputs.
    /// 
    /// Example usage when using outside of the attribute:
    /// 
    /// BenchmarkController benchmarkController = new BenchmarkController();
    /// benchmarkController.AddAction(new CPUUsageMeasurement());
    /// benchmarkController.StartAll();
    /// benchmarkController.StopAll();
    /// ReportController reportController = new ReportController(new TestContextOutputReporter());
    /// reportController.ReportAll($"-> This should be visible as well in test explorer: {benchmarkController.Results.ToString()}");
    /// /// </summary>
    public class BenchmarkController : IBenchmarkController
    {
        private readonly BenchmarkResultsHistory _results = new BenchmarkResultsHistory();
        private readonly List<IBenchmarkAction> _actions = new List<IBenchmarkAction>();
        public void AddAction(IBenchmarkAction action)
        {
            // avoid duplicates. It makes no sense (at least for now) to add the same benchmark action (with the same type) multiple times.
            // otherwise this can conflict with e.g. the default constructor of the BenchmarkThisAttribute to add its default IBenchmarkAction.
            if (!_actions.Any(a => a.GetType() == action.GetType()))
            {
                _actions.Add(action);
            }
        }

        public BenchmarkController()
        {

        }

        public BenchmarkController(params IBenchmarkAction[] benchmarkActions )
        {
            AddActions(benchmarkActions);
        }

        public void AddActions(params IBenchmarkAction[] benchMarkActions)
        {
            foreach (var action in _actions)
            {
                AddAction(action);
            }
        }

        public BenchmarkController WithActions(params IBenchmarkAction[] benchmarkActions)
        {
            AddActions(benchmarkActions);
            return this;
        }

        /// <summary>
        /// Starts all benchmark actions.
        /// </summary>
        public void StartAll()
        {
            foreach (IBenchmarkAction action in _actions)
            {
                action.Start();
            }
        }

        /// <summary>
        /// This collect the results of the stopped actions.
        /// </summary>
        public void StopAll()
        {
            foreach (IBenchmarkAction action in _actions)
            {
                action.Stop();
                var result = action.GetResult();
                var actionType = action.GetType();

                if (!_results.ContainsKey(actionType))
                {
                    _results[actionType] = new List<BenchmarkResult>();
                }
                _results[actionType].Add(result);
            }
        }

        public BenchmarkResultsHistory Results { get { return _results; } }
    }
}
