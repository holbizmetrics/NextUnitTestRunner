using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Provides an AutoFixture.NextUnit.Customize Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class CustomizeAttribute : Attribute, IParameterCustomizationSource
    {
        public abstract ICustomization GetCustomization(ParameterInfo parameter);
    }
}
