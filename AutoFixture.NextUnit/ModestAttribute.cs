using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// An attribute that can be applied to parameters in an AutoDataAttribute-driven Theory to indicate that
    /// the parameter value should be created using the most modest constructor that can be satisfied by an IFixture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ModestAttribute : CustomizeAttribute
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
            return new ConstructorCustomization(parameter.ParameterType, new ModestConstructorQuery());
        }
    }
}