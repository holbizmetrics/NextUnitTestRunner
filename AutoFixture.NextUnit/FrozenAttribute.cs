using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Will provide a FrozenAttribute.
    /// </summary>
    public class FrozenAttribute : CustomizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            throw new NotImplementedException();
        }
    }
}
