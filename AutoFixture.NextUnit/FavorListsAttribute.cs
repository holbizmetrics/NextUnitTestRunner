using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Provides an AutoFixture.NextUnit.FavorListsAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FavorListsAttribute : CustomizeAttribute
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
            return new ConstructorCustomization(parameter.ParameterType, new ListFavoringConstructorQuery());
        }
    }
}
