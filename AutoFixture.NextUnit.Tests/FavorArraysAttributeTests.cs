using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class FavorArraysAttributeTests
    {
        [Test]
        public void FavorArraysAttributeTest([FavorArrays] string[] stringArray)
        {

        }
    }
}
