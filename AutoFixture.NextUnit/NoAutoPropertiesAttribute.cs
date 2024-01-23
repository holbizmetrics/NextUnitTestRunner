// AutoFixture.NextUnit.NoAutoPropertiesAttribute
using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class NoAutoPropertiesAttribute : CustomizeAttribute
    {
        /// <summary>
        /// 
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
