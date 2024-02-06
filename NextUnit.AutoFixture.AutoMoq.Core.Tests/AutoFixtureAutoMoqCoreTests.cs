using NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes;
using NextUnit.Core.TestAttributes;

namespace NextUnit.AutoFixture.AutoMoq.Core.Tests
{
    public class AutoFixtureAutoMoqCoreTests
    {
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoFixture))]
        public void AutoMoqDataAttributeTest()
        {

        }
    }
}
