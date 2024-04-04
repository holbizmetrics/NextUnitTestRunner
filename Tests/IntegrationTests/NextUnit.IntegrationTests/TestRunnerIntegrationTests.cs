using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.IntegrationTests
{
    public class TestRunnerIntegrationTests
    {
        [Test]
        public void TestRunnerEvents_AreRaised_WhenTestRun_WasExecuted()
        {
            // Arrange
            TestClass testClass = new TestClass();
            ITestRunner5 testRunner5 = new TestRunner5();
            
            // Act
            testRunner5.Run(testClass);

            // Assert
            Assert.EventWasRaised(testRunner5, "AfterTestRun");
            Assert.EventWasRaised(testRunner5, "BeforeTestRun");
        }

        [Test]
        public void TestRunnerFinishesTestRunCompletely()
        {
            ITestRunner5 testRunner5 = new TestRunner5();
            testRunner5.TestRunFinished += (s, e) => { };
            testRunner5.Run(typeof(TestClass));
        }

        public class TestClass
        {
            [Test]
            public void Test()
            {

            }
        }
    }
}
