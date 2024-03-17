using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.TestRunner.Tests
{
    public class TestRunner5Tests
    {
        [Test]
        public void TestRunner5RunTest()
        {
            bool finished = false;
            ITestRunner5 testRunner5 = new TestRunner5();
            testRunner5.TestRunFinished += (s, e) => { finished = true; };
            //testRunner5.Run(typeof(NestedClassForTestRun));
            testRunner5.Dispose();
            testRunner5 = null;
        }

        public class NestedClassForTestRun
        {
            [Test]
            public void RunThisTest()
            {

            }
        }
    }
}
