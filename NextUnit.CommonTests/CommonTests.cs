using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.CommonTests
{
    public class Tests
    {
        [Test]
        public void Setup()
        {
        }

        [Group(nameof(CommonTests))]
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Group(nameof(CommonTests))]
        [Test]
        public void Test2()
        {

        }
    }
}