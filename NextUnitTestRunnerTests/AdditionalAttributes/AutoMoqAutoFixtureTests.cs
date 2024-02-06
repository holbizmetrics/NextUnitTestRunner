using System.ComponentModel;
using AutoFixture.NextUnit;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.TestRunnerTests.AdditionalAttributes
{
    public class AutoMoqAutoFixtureTests
    {
        /// <summary>
        /// Tests the AutoFixture AutoData Attribute
        /// Each 
        /// </summary>
        /// <param name="n1"></param>
        [Test]
        [AutoData]
        public void AutoDataTest(int n1, int n2, int n3, string test)
        {

        }

        [Test]
        [InlineAutoData(1, 2, "do it now")]
        public void InlineDataTest(int param1, int param2, string text)
        {

        }

        /// <summary>
        ///instantiation and everything else that can be implicitly derived is done automatically
        ///Even if class Calculator changes.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="calculator"></param>
        [Test, AutoMoqDataNextUnit]
        [Category("Advanced")]
        public void TestCalculatorWithAutoMoqAttribute(int x, int y, Calculator calculator)
        {
            Assert.IsTrue(x + y == calculator.Sum(x, y));
        }
    }
}
