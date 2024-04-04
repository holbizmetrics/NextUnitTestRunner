using NextUnit.Benchmarking.Measurements;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnit.Benchmarking.Tests
{
    public class NextUnitBenchmarkingTests
    {
        [Test]
        [BenchmarkRepeat(10)]
        [BenchmarkThis]
        public void Test()
        {
            // TODO: this has to create an output in the test explorer.
            // To make this work for now, the following has to be accomplished (sequence of the mentioned points is not yet, considered:
            //
            // 1. ITestOutput usage has to really make used text appear in test explorer.
            // 2. Hook point has to be called by the Test Executor (Test Execution Pipeline) from TestRunner.
            // 3. The attributes containing ITestContext have to be executed by the whole engine.
            //
            // I guess neither of this would be happening, yet.
            Trace.WriteLine("Hello");
        }

        [Test]
        [BenchmarkThis]
        public void UseOnlyOneBenchmarkThisAttributeTest()
        {
            // This should also result in an output additionally added to the test explorer's output.
            BenchmarkController benchmarkcontroller = new BenchmarkController();
            benchmarkcontroller.AddAction(new CPUUsageMeasurement());

            ReportController reportcontroller = new ReportController(new TestContextOutputReporter());
            reportcontroller.ReportAll(benchmarkcontroller.Results.ToString());
        }

        [Test]
        [BenchmarkRepeat(10)]
        public void UseOnlyOneBenchmarkRepeatAttributeTest()
        {

        }

        [Test]
        [BenchmarkThis]
        [BenchmarkThis]
        public void UseSeveralBenchmarkThisAndNoRepeatAttribute()
        {

        }

        [Test]
        [BenchmarkRepeat(10)]
        [BenchmarkThis]
        [BenchmarkThis]
        public void UseSeveralBenchmarkThisAttributesAndBenchmarkRepeatAttributeTest()
        {

        }
    }
}
