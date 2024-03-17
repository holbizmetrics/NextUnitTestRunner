using NextUnit.Core;
using NextUnit.Core.Combinators;
using System.Reflection;

namespace NextUnit.TestRunner.TestRunners.TestRunner5
{
    public delegate TestResult TestDelegate((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition);
    public class TestExecutor
    {
        public IInstanceCreationBehavior InstanceCreationBehavior { get; set; } = null;
        public ICombinator UsedCombinator { get; set; } = null;
        
        public TestDelegate TestExecutionPipeline;
        public TestResult TestResult { get; set; } = TestResult.Empty;
        public TestExecutor()
        {
            TestExecutionPipeline = testc => { return TestResult; };
        }

        public TestExecutor(IInstanceCreationBehavior instanceCreationBehavior, ICombinator usedCombinator)
            : this()
        {
            this.With(instanceCreationBehavior);
            this.With(usedCombinator);
        }

        public TestExecutor With(ICombinator combinator)
        {
            UsedCombinator = combinator;
            return this;
        }

        public TestExecutor With(IInstanceCreationBehavior instanceCreationBehavior)
        {
            InstanceCreationBehavior = instanceCreationBehavior;
            return this;
        }

        public void AddToPipeline(TestDelegate newStep)
        {
            TestExecutionPipeline += newStep;
        }

        public void RemoveFromPipeline(TestDelegate step)
        {
            TestExecutionPipeline -= step;
        }

        public TestResult Execute((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) test)
        {
            return TestExecutionPipeline.Invoke(test);
        }

        /// <summary>
        /// Return how many steps had been added.
        /// </summary>
        /// <returns></returns>
        public int StepCount()
        {
            if (TestExecutionPipeline != null)
            {
                return TestExecutionPipeline.GetInvocationList().Length;
            }
            return 0;
        }

        /// <summary>
        /// Return the set steps
        /// </summary>
        public TestDelegate[] Steps
        {
            get
            {
                return TestExecutionPipeline?.GetInvocationList().Cast<TestDelegate>().ToArray();
            }
        }
    }
}
