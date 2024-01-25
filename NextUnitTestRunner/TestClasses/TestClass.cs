using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.NextUnit;
using Moq;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Net.Http.Headers;
using AutoFixture.Kernel;

namespace NextUnit.TestRunner.TestClasses
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

        public class MyClass
        {

        }

        [Test]
        [InlineAutoData]                  // all args will be provided by autofixture
        [InlineAutoData("FOO")]           // BAR & sut will be provided by autofixture
        [InlineAutoData("FOO", "BAR")]    // sut will be provided bt autofixture
        public void InlineAutoDataTest(string foo, string bar, MyClass sut)
        {

        }

        [Test, RunInThreadAttribute]
        public void ThreadingTest()
        {

        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
                : this(null)
            {
            }

            protected AutoMoqDataAttribute(Action<IFixture>? cfg)
                : base(() =>
                {
                    var fixture = new Fixture();
                    fixture.Customize(new AutoMoqCustomization
                    {
                        ConfigureMembers = true,
                        GenerateDelegates = true,
                    });
                    cfg?.Invoke(fixture);
                    return fixture;
                })
            {
            }
        }

        public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
        {
            public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects)
            {
            }
        }

        public interface ISomeInterface
        {

        }

        public class MySut
        {

        }

        [InlineAutoMoqData(3, 4)]
        [InlineAutoMoqData(33, 44)]
        [InlineAutoMoqData(13, 14)]
        public void SomeUnitTest(int DataFrom, int OtherData, [Frozen] Mock<ISomeInterface> theInterface, MySut sut)
        {
        }

        [Test]
        [AutoData]
        public void TestFixtureTest(int n1)
        {
            var fixture = new Fixture();
            var instance = fixture.Create<int>();

            Trace.WriteLine($"Generated value using AutoData, n1: {n1} + fixture created int: result = {n1 + instance}");
        }
    }
}
