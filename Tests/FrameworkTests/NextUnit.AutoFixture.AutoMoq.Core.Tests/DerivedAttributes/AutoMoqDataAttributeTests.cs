using Moq;
using NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.AutoFixture.AutoMoq.Core.Tests.DerivedAttributes
{
    public class AutoMoqDataAttributeTests
    {
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoFixture))]
        public void AutoMoqDataAttributeTest(Mock<InvalidCastException> ex)
        {
            int a = 0;
            int b = 0;
            Assert.IsNotNull(ex.Name);
            Assert.Throws<DivideByZeroException>(() => { int c = a / b; });
        }
    }
}