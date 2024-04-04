using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class FrozenAttributeTests
    {
        [Test]
        public void FrozenAttributeTest([Frozen] int frozen)
        {

        }
    }
}
