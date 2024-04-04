using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace AutoFixture.NextUnit.Tests
{
    public class BlubAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return null;
        }
    }

    public class CustomizeAttributeTests
    {
        [Test]
        public void CustomizeAttributeTestWithoutParameterAttribute()
        {

        }

        [Test, AutoData]
        public void CustomizeAttributeTestAsParameterTest([Blub] int n1)
        {
        
        }
    }
}
