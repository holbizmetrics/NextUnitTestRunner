using System.Reflection;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to hook into several points into the test pipeline execution engine (aka TestExecutor as setup in TestRunner)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class TestHookAttribute : CommonTestAttribute, ITestHook
    {
        public TestResult TestResult { get; set; } = TestResult.Empty;
        public TestHookAttribute()
        {

        }

        public virtual TestResult AfterTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testCase)
        {
            return TestResult;
        }

        public virtual TestResult BeforeTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testCase)
        {
            return TestResult;
        }
    }

    public interface ITestHook
    {
        public TestResult AfterTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testCase);
        public TestResult BeforeTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testCase);
    }
}
