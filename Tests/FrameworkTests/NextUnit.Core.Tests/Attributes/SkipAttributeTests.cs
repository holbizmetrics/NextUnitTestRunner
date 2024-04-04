using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class SkipAttributeTests
    {
        [Test]
        [Skip]
        public void SkipThisTest()
        {

        }
    }
}
