using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class FuzzingAttributeTests
    {
        [Test]
        [Group(nameof(FuzzingAttribute))]
        [Fuzzing]
        public void FuzzingAttributeTest(int n1, int n2)
        {
            int result = n1 + n2;
            System.Console.WriteLine($"n1 + n2: {n1 + n2}");
        }
    }
}