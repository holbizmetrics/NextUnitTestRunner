using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class PermutationAttributeTests
    {
        [Test]
        [Permutation]
        [Group(nameof(PermutationAttribute))]
        public void PermutationAttributeTest(int a, int b)
        {

        }
    }
}
