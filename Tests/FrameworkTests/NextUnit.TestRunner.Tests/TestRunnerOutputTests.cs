using NextUnit.Core.TestAttributes;
using NextUnit.TestEnvironment;

namespace NextUnit.TestRunner.Tests
{
    public class TestRunnerOutputTests
    {
        [Test]
        public void TestRunnerOutputTest()
        {
            TestExecutionContext.TestOutput.LogMessage("Test");
        }

        /// <summary>
        /// If this works the message given in the attribute should be visible in the test explorer later on.
        /// </summary>
        [TestExecutionContextLog("Log this message")]
        public void LetsSeeIfThisLogs()
        {

        }

        public class TestExecutionContextLogAttribute : CommonTestAttribute
        {
            public TestExecutionContextLogAttribute()
            {

            }

            public TestExecutionContextLogAttribute(string message)
            {
                LogMessage(message);
            }

            private void LogMessage(string message)
            {
                TestExecutionContext.TestOutput.LogMessage($"{message}");
            }
        }
    }
}
