using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class ModestAttributeTests
    {
        [Test]
        public void ModestAttributeTest([Modest] int i)
        {

        }
    }
}
