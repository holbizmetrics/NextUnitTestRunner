using AutoFixture.NextUnit.DerivedAttributes;
using NextUnit.Core.TestAttributes;

namespace NextUnit.AutoFixture.AutoMoq.Core.Tests.DerivedAttributes
{
    public class AutoMoqDataSimpleAttributeTests
    {
        [Test]
        [AutoMoqDataSimple(true, true)]
        public void AutoMoqDataSimpleAttributeTest_ConfigureMembers_CreateDelegates(int parameter1, int parameter2)
        {

        }

        [Test]
        [AutoMoqDataSimple(true, false)]
        public void AutoMoqDataSimpleAttributeTest_ConfigureMembers(int parameter1, int parameter2)
        {

        }

        [Test]
        [AutoMoqDataSimple(false, false)]
        public void AutoMoqDataSimpleAttributeTest_CreateDelegates(int parameter1, int parameter2)
        {

        }

        [Test]
        [AutoMoqDataSimple(false, true)]
        public void AutoMoqDataSimpleAttributeTest(int parameter1, int parameter2)
        {

        }
    }
}
