using NextUnit.Core.Asserts;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Extensions
{
    public class ObjectExtensions
    {
        [Test]
        public void ObjectExtensionsTest()
        {
            object name = "Name";
            string nameAsString = name.As<string>();

            Assert.AreEqual(name, nameAsString);
        }
    }
}
