using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class RunAfterAttributeTests
    {
        [Test]
        [Group(nameof(RunAfterAttribute))]
        [RunAfter("")]
        public void RunAfterAttributeTest()
        {

        }
    }
}
