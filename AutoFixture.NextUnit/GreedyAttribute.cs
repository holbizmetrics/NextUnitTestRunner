using AutoFixture.Kernel;
using System.Reflection;
using System.Reflection.Metadata;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// An attribute that can be applied to parameters in an AutoDataAttribute-driven TestCase to indicate that
    /// the parameter value should be created using the most greedy constructor that can be satisfied by an IFixture.
    /// </summary>
    public class GreedyAttribute : CustomizeAttribute
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
                throw new ArgumentNullException(nameof(parameter));
            }

            return new ConstructorCustomization(parameter.ParameterType, new GreedyConstructorQuery());
        }
    }
}
