using NextUnitTestRunner.TestAttributes;
using System.Diagnostics;

namespace NextUnitTestRunner
{
    public class TestClass
    {
        [Test("Hallo")]
        [Random(11, 2222, 5)]
        public void Test(int param1, int param2)
        {
            Trace.WriteLine($"We've been called with those parameters: {param1}, {param2}");
            //Assert.IsTrue(false);
        }

        [ExtendedTest()]
        public void Test2()
        {
            Trace.WriteLine("We've been called as well");
        }

        [Test]
        public void Test3()
        {

        }
    }
}
