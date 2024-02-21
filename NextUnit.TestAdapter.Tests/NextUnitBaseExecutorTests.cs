using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NextUnit.Core.Accessors;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.TestAdapter.Tests
{
    public class NextUnitBaseExecutorTests
    {
        private NextUnitBaseExecutor nextUnitBaseExecutor = new NexUnitBaseExecutorInheritedTest();
        
        [Test]
        public void TestRunDirectoryTest()
        {
            string testRunDirectory = "";
            Assert.AreEqual(testRunDirectory, nextUnitBaseExecutor.TestRunDirectory);
        }

        [Test]
        public void IsDataCollectionEnabledTest()
        {
            bool isDataCollectionEnabled = false;
            Assert.AreEqual(isDataCollectionEnabled, nextUnitBaseExecutor.IsDataCollectionEnabled);
        }


        [Test]
        public void InIsolationTest()
        {
            bool InIsolation = false;
            Assert.AreEqual(InIsolation, nextUnitBaseExecutor.InIsolation);
        }

        [Test]
        public void IsBeingDebuggedTest()
        {
            bool isBeingDebugged = false;
            Assert.AreEqual(isBeingDebugged, nextUnitBaseExecutor.IsBeingDebugged);
        }

        [Test]
        public void KeepAliveTest()
        {
            bool keepAliveExpected = false;
            Assert.AreEqual(keepAliveExpected, nextUnitBaseExecutor.KeepAlive);
        }

        [Test]
        public void SolutionDirectoryTest()
        {
            string expectedSolutionDirectory = "";
            Assert.AreEqual(expectedSolutionDirectory, nextUnitBaseExecutor.SolutionDirectory);
        }

        [Test]
        public void RunSettingsTest()
        {
            RunSettings runSettings = new RunSettings();
            Assert.AreEqual(runSettings, nextUnitBaseExecutor.RunSettings);
        }

        [Test]
        public void ExecuteTest()
        {
            AccessWrapper wrapper = new AccessWrapper(nextUnitBaseExecutor);
            TestCase testCase = new TestCase();
            wrapper.AsDynamic().ExecuteTest(testCase);
        }
    }

    public class NexUnitBaseExecutorInheritedTest : NextUnitBaseExecutor
    {
        protected override TestResult ExecuteTest(TestCase testCase)
        {
            return base.ExecuteTest(testCase);
        }
    }

}
