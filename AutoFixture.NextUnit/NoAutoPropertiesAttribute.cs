using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// An attribute that can be applied to parameters in an AutoDataAttribute-driven Theory to 
    /// indicate that the parameter value should not have properties auto populated when the
    /// IFixture creates an instance of that type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class NoAutoPropertiesAttribute : CustomizeAttribute
    {
        /// <summary>
        /// Gets the customization.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }
            return new NoAutoPropertiesCustomization(parameter.ParameterType);
        }
    }
}
