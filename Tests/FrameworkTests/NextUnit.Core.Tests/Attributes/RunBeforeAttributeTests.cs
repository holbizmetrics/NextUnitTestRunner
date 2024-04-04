using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class RunBeforeAttributeTests
    {
        [Test]
        [Group(nameof(RunBeforeAttribute))]
        //[Group("RunBeforeAttribute")]
        [RunBefore("2024-05-01T00:00:00")]
        public void RunBeforeAttributeTest()
        {

        }
    }
}
