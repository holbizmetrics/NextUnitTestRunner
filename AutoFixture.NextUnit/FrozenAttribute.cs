using AutoFixture.Kernel;
using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// An attribute that can be applied to parameters in an AutoDataAttribute-driven Theory to indicate that
    /// the parameter value should be frozen so that the same instance is returned every time the IFixture creates an instance of
    /// that type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FrozenAttribute : CustomizeAttribute
    {
        public Matching By { get; set; } = Matching.ExactType; // Default matching strategy

        /// <summary>
        /// Gets the customization.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new FreezeOnMatchCustomization(parameter.ParameterType, new TypeMatcher(By));
        }
    }

    public class TypeMatcher : IRequestSpecification
    {
        private readonly Matching _matchingCriteria;

        public TypeMatcher(Matching matchingCriteria)
        {
            _matchingCriteria = matchingCriteria;
        }

        public bool IsSatisfiedBy(object request)
        {
            var requestType = request as Type;
            if (requestType == null)
            {
                return false;
            }
            // Implement matching logic based on _matchingCriteria
            // For example, for exact type match:
            if (_matchingCriteria == Matching.ExactType)
            {
                // Logic for exact type match
            }
            // Add more conditions for other matching criteria as needed

            return false;
        }
    }
}
