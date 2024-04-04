using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Provides an AutoFixture.NextUnit.Customize Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class CustomizeAttribute : CommonTestAttribute, IParameterCustomizationSource
    {
        public abstract ICustomization GetCustomization(ParameterInfo parameter);
    }
}
