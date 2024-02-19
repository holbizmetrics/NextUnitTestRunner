using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.CommonTests
{
    /// <summary>
    /// The common tests will be tests that contain different things.
    /// It won't contain things of testing the framework necessarily
    /// in general. BUT other things you could do using the framework.
    /// </summary>
    public class Tests
    {
        [Test]
        [Group(nameof(CommonTests))]
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