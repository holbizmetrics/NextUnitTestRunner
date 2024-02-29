using NextUnit.Core;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.TestRunner.TestRunners.NewFolder
{
    public partial class TestRunner5
    {
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;

        /// <summary>
        /// This will be fired before the test is run.
        /// </summary>
        /// <param name="e"></param>
        protected void OnBeforeTestRun(ExecutionEventArgs e)
        {
            BeforeTestRun?.Invoke(this, e);
        }

        /// <summary>
        /// This will be fired after the test was run.
        /// </summary>
        /// <param name="e"></param>
        protected void OnAfterTestRun(ExecutionEventArgs e)
        {
            AfterTestRun?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if a test is executing.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestExecuting(ExecutionEventArgs e)
        {
            TestExecuting?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if a test run is started.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestRunStarted(ExecutionEventArgs e)
        {
            TestRunStarted?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if a test run is ending.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestRunFinished(ExecutionEventArgs e)
        {
            TestRunFinished?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if an error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected void OnError(ExecutionEventArgs e)
        {
            ErrorEventHandler?.Invoke(this, e);
        }

        /// <summary>
        /// Not very efficient. But it works for now.
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        protected TestResult BeforeTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition)
        {
            TestHookAttribute testHookAttribute = testDefinition.methodInfo.GetCustomAttribute<TestHookAttribute>();
            if (testHookAttribute != null)
            {
                testHookAttribute.BeforeTestRunExecution(testDefinition);
            }
            return TestResult.Empty;
        }

        /// <summary>
        /// Not very efficient. But it works for now.
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        protected TestResult AfterTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition)
        {
            TestHookAttribute testHookAttribute = testDefinition.methodInfo.GetCustomAttribute<TestHookAttribute>();
            if (testHookAttribute != null)
            {
                testHookAttribute.AfterTestRunExecution(testDefinition);
            }
            return TestResult.Empty;
        }
    }
}