using AutoFixture.NextUnit;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnitTestRunner.TestClasses
{
    /// <summary>
    /// The goal is to make all those tests in here working properly.
    /// </summary>
    public class TestClass
    {
        [Test("Hallo")]
        //[Random(11, 2222, 5)]
        [InjectData(1, 2)]
        public void TestRandomAndInjectData(int param1, int param2)
        {
            Trace.WriteLine($"We've been called with those parameters: {param1}, {param2}");
            //Assert.IsTrue(false);
        }

        /// <summary>
        /// This has to repeat the Test n times [Repetitions(n)]
        /// </summary>
        [Test]
        [Repetitions(7)]
        public void RepetitionsTest()
        {

        }

        /// <summary><
        /// This test has to fail if it takes longer then n to execute. [Timeout(n)]
        /// </summary>
        [Timeout(1000)]
        public void Timeout()
        {

        }

        /// <summary>
        /// Tests an extended test attribute.
        /// </summary>
        [ExtendedTest()]
        public void ExtendedTest()
        {
            Trace.WriteLine("We've been called as well");
        }

        // Autofixture Automoq Tests

        /// <summary>
        /// 
        /// </summary>
        [Test, AutoData]
        public void AutoDataTest(int n1, int n2)
        {

        }

        [Test, InlineAutoData]
        public void InlineDataTest()
        {
        }

        [Test, RunInThread]
        public void ThreadingTest()
        {

        }
    }
}
