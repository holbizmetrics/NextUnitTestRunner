using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class FavorArraysAttributeTests
    {
        [Test, AutoData]
        public void FavorArraysAttributeTest([FavorArrays] string[] stringArray)
        {

        }
    }
}
