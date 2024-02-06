using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class AutoDataAttributeTests
    {
        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataNoParametersInjectingTest()
        {

        }

        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataIncludingSimpleParametersTests(int intParameter, bool parameter, string[] stringArray, List<string> stringList)
        {

        }

        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataIncludingComplexParametersTests(Group group)
        {

        }
    }

    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public int orderID { get; set; } = -1;
    }
}
