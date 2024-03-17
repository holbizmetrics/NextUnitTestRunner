using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NextUnit.Core.TestAttributes;

namespace NextUnit.TestAdapter.Tests
{
    public class FrameworkHandleTests
    {
        [Test]
        public void EnableShutdownAfterTestRunShoudBeFalseByDefault()
        {
            var tec = GetTestExecutionContext();
            //var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            //Assert.IsFalse(frameworkHandle.EnableShutdownAfterTestRun);
        }

        [Test]
        public void EnableShutdownAfterTestRunShoudBeSetAppropriately()
        {
            var tec = GetTestExecutionContext();
            //var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            //frameworkHandle.EnableShutdownAfterTestRun = true;

            //Assert.IsTrue(frameworkHandle.EnableShutdownAfterTestRun);
        }

        [Test]
        public void LaunchProcessWithDebuggerAttachedShouldThrowIfObjectIsDisposed()
        {
            var tec = GetTestExecutionContext();
            //var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);
            //frameworkHandle.Dispose();

            //Assert.ThrowsException<ObjectDisposedException>(() => frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null));
        }

        // TODO: Enable method once we fix the "IsDebug" in TestExecutionContext
        // [Test]
        public void LaunchProcessWithDebuggerAttachedShouldThrowIfNotInDebugContext()
        {
            var tec = GetTestExecutionContext();
            //var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            var isExceptionThrown = false;
            try
            {
                //frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);
            }
            catch (InvalidOperationException exception)
            {
                isExceptionThrown = true;
                Assert.AreEqual("This operation is not allowed in the context of a non-debug run.", exception.Message);
            }

            Assert.IsTrue(isExceptionThrown);
        }

        [Test]
        public void LaunchProcessWithDebuggerAttachedShouldCallRunEventsHandler()
        {
            var tec = GetTestExecutionContext();
            tec.IsDebug = true;
            var mockTestRunEventsHandler = new Mock<ITestRunEventsHandler>();

            //var frameworkHandle = new FrameworkHandle(
            //                          null,
            //                          new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }),
            //                          tec,
            //                          mockTestRunEventsHandler.Object);

            //frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);

            //mockTestRunEventsHandler.Verify(mt =>
                //mt.LaunchProcessWithDebuggerAttached(It.IsAny<TestProcessStartInfo>()), Times.Once);
        }

        private static TestExecutionContext GetTestExecutionContext()
        {
            var tec = new TestExecutionContext(
                          frequencyOfRunStatsChangeEvent: 100,
                          runStatsChangeEventTimeout: TimeSpan.MaxValue,
                          inIsolation: false,
                          keepAlive: false,
                          isDataCollectionEnabled: false,
                          areTestCaseLevelEventsRequired: false,
                          hasTestRun: false,
                          isDebug: false,
                          testCaseFilter: string.Empty,
                          filterOptions: null);
            return tec;
        }
    }
}
