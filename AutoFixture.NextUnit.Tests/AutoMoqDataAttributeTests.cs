using NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes;
using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class AutoMoqDataAttributeTests
    {
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_NoParametersInMethodTest()
        {

        }

        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_SimpleParametersInMethodTest()
        {

        }

        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_ComplexParametersInMethodTest()
        {

        }

        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_OnlyMockInterfaceInMethodTest()
        { 
        }

        [Test]
        [AutoMoqData(true)]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_MockInterfaceAndComplexParametersInMethod_SetupProperties_Test()
        {

        }
    }
}