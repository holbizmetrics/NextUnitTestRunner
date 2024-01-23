// AutoFixture.NextUnit.ModestAttribute
using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ModestAttribute : CustomizeAttribute
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
            return new ConstructorCustomization(parameter.ParameterType, new ModestConstructorQuery());
        }
    }
}