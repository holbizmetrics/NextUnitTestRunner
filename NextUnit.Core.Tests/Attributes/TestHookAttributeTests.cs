using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.Tests.Attributes
{
    public class TestHookAttributeTests
    {
        [Test]
        [CustomTestHook]
        public void TestHookAttributeTest()
        {

        }
    }

    public class CustomTestHookAttribute : TestHookAttribute
    {
        public override TestResult AfterTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) TestCase)
        {
            return base.AfterTestRunExecution(TestCase);
        }

        public override TestResult BeforeTestRunExecution((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testCase)
        {
            return base.BeforeTestRunExecution(testCase);
        }
    }
}
