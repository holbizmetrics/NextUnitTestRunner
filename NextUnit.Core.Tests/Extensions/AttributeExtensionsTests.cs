using NextUnit.Core.Asserts;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Extensions
{
    public class AttributeExtensionsTests
    {
        public void AttributeExtensionsTest()
        {
            CommonTestAttribute[] testAttributes = new CommonTestAttribute[] { new DebugAttribute(), new DebuggerBreakAttribute(), new FuzzingAttribute() };
            Assert.IsTrue(testAttributes.AnyIsOf(typeof(DebugAttribute)));
        }
    }
}
