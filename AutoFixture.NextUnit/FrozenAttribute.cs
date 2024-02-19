using AutoFixture.Kernel;
using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Will provide a FrozenAttribute.
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
