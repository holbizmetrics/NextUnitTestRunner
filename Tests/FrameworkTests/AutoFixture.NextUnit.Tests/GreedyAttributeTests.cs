using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class GreedyAttributeTests
    {
        [Test]
        public void GreedyAttributeTest([Greedy] int test)
        {

        }
    }
}
