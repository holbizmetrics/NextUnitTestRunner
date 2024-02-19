using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Provides an AutoFixture.NextUnit.GreedyAttribute.
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
