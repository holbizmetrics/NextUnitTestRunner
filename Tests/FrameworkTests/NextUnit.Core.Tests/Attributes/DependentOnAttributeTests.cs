using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class DependentOnAttributeTests
    {
        /// <summary>
        /// This test should pass if the dependent on tests pass.
        /// Subsequentially the sequence in which the tests in the attribute pass, is not important.
        /// It's only important that all pass before the test itself can pass.
        /// 
        /// So in other words: If one of the tests already failed in DependentOn, then this test will fail, too.
        /// </summary>
        [Test]
        [DependentOn("Test1", "Test2", "Test3")]
        public static void DependentOnAttributeTest()
        {

        }

        [Test]
        [DependentOn(nameof(Test1), nameof(Test2), nameof(Test3))]
        public static void DependendOnAttributeTest()
        {

        }

        [Test]
        public static void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public static void Test2()
        {

        }

        [Test]
        public static void Test3()
        {

        }

        [Test]
        public void Test()
        {

        }
    }
}
