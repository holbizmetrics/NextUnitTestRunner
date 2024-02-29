using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class NoAutoPropertiesAttributeTests
    {
        [Test, AutoData]
        public void NoAutoPropertiesAttributeTest([NoAutoProperties] int param1)
        {

        }
    }
}
