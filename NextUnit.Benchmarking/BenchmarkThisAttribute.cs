using System.Text;
using NextUnit.Benchmarking.Measurements;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Benchmarking
{
    // TODO: This attribute implementation demands that the hook context points work correctly.
    /// <summary>
    /// This benchmark attribute can be used to measure performance aspects.
    /// 
    /// Things to consider:
    /// 
    /// Velocity
    /// --------
    /// - Ideally run this in release mode without an attached debugger.
    ///   Run it the same way (e.g. not sometimes with debugger, sometimes without, sometimes release or debug and so on.)
    /// 
    /// Reliability
    /// -----------
    /// - Please be aware that ideally tests marked with this Benchmark attribute should be executed in the same environment.
    ///   On your local computer you may be forced to abort tests, shut down the computer (which will also abort tests), etc.
    ///   
    /// Output
    /// ------
    /// If a test fails, no output will occur.
    /// Because that means the AfterTest method will not be called from outside.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class BenchmarkThisAttribute : Attribute, ItestRun
    {
        public static int countCall = 0;
        private BenchmarkController BenchMarkcontroller { get; } = new BenchmarkController();
        private ReportController ReportController { get; } = new ReportController();

        private static Type[] AllTypes = new Type[] {
            typeof(EFQueryMeasurement),
            typeof(GCCollectionMeasurement),
            typeof(HandleCountMeasurement),
            typeof(MemoryUsageMeasurement),
            typeof(TimeMeasurement),
            typeof(CPUUsageMeasurement)
        };

        private static Type[] ReporterTypes = new Type[]
        {
            typeof(DebugConsoleBenchmarkReporter),
            typeof(TestContextOutputReporter)
        };

        public BenchmarkThisAttribute()
        {
            countCall++;
            InitializeDefault();
        }

        /// <summary>
        /// Initialize default actions and report outputs.
        /// </summary>
        private void InitializeDefault()
        {
            AddUsedEntities(AllTypes);
            this.WithReporters(new TestContextOutputReporter());
        }

        #region fluent syntax implementation
        public BenchmarkThisAttribute WithReporters(params IBenchmarkReporter[] benchmarkReporters)
        {
            foreach (var benchmarkReporter in benchmarkReporters)
            {
                ReportController.AddReporter(benchmarkReporter);
            }
            return this;
        }
        #endregion fluent syntax implementation

        public BenchmarkThisAttribute(params Type[] addableTypes)
        {
            AddUsedEntities(addableTypes);
        }

        private void AddUsedEntities(Type[] addableTypes)
        {
            foreach (var type in addableTypes)
            {
                object supposedToBeBenchmarkActionOrBenchmarkReporter = (ContainsInterface(type, typeof(IBenchmarkAction)) || ContainsInterface(type, typeof(IBenchmarkReporter))) ? Activator.CreateInstance(type) : null;

                if (supposedToBeBenchmarkActionOrBenchmarkReporter == null) continue; // we can't add this type. Skip it.
                if (supposedToBeBenchmarkActionOrBenchmarkReporter is IBenchmarkAction)
                {
                    BenchMarkcontroller.AddAction((IBenchmarkAction)supposedToBeBenchmarkActionOrBenchmarkReporter);
                }
                else if (supposedToBeBenchmarkActionOrBenchmarkReporter is IBenchmarkReporter)
                {
                    ReportController.AddReporter((IBenchmarkReporter)supposedToBeBenchmarkActionOrBenchmarkReporter);
                }
                else
                {
                    throw new ArgumentException($"{type.Name} does not implement IBenchmarkAction.");
                }
            }
        }

        public static bool ImplementsInterface(Type type, Type interfaceType)
        {
            return type.IsAssignableFrom(interfaceType);
        }

        public static bool ContainsInterface(Type type, Type interfaceType)
        {
            return type.GetInterfaces().Contains(typeof(IBenchmarkAction));
        }

        protected virtual void AddDefaultActions()
        {
            BenchMarkcontroller.AddAction(new TimeMeasurement());
        }

        protected virtual void AddDefaultReporters()
        {
            ReportController.AddReporter(new TestContextOutputReporter());
        }

        public void BeforeTestExecution()
        {
            BenchMarkcontroller.Results.Clear();
            BenchMarkcontroller.StartAll();
        }

        public void AfterTestExecution()
        {
            BenchMarkcontroller.StopAll();

            StringBuilder resultsPerTypeReportText = new StringBuilder();
            resultsPerTypeReportText.AppendLine
($@"-----------
single run
-----------
");
            foreach (KeyValuePair<Type, List<BenchmarkResult>> resultsPerType in this.BenchMarkcontroller.Results)
            {
                resultsPerTypeReportText.AppendLine(resultsPerType.Value.First().ToString());
            }
            ReportController.ReportAll(resultsPerTypeReportText.ToString());
        }

        public void BeforeTestRun()
        {
            throw new NotImplementedException();
        }

        public void AfterTestRun()
        {
            throw new NotImplementedException();
        }
    }
}