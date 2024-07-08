using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;
namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class AllCombinationsAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var parameterInfos = @delegate.GetMethodInfo().GetParameters();
            var allParameterValues = parameterInfos.Select(pi => GetValuesForParameter(pi)).ToList();

            // Generate all combinations of these parameter values
            var allCombinations = GenerateAllCombinations(allParameterValues, 0);

            // Invoke the test method with each combination of parameter values
            foreach (var combination in allCombinations)
            {
				Invoker.Invoke(@delegate, testInstance, combination.ToArray()); //testMethod.Invoke(testInstance, @delegate, combination.ToArray());
			}
        }

        private IEnumerable<IEnumerable<object>> GenerateAllCombinations(List<IEnumerable<object>> allParameterValues, int currentIndex)
        {
            if (currentIndex >= allParameterValues.Count)
            {
                yield return Enumerable.Empty<object>();
                yield break;
            }

            var currentValues = allParameterValues[currentIndex];
            var combinationsOfRest = GenerateAllCombinations(allParameterValues, currentIndex + 1);

            foreach (var value in currentValues)
            {
                foreach (var combination in combinationsOfRest)
                {
                    yield return new[] { value }.Concat(combination);
                }
            }
        }

        private IEnumerable<object> GetValuesForParameter(ParameterInfo parameterInfo)
        {
            // Check for [Values] attribute
            var valuesAttribute = parameterInfo.GetCustomAttribute<ValuesAttribute>();
            if (valuesAttribute != null)
            {
                return valuesAttribute.Values;
            }

            // Fallback to a default strategy
            return GetDefaultValuesForType(parameterInfo.ParameterType);
        }

        private IEnumerable<object> GetDefaultValuesForType(Type type)
        {
            // Handle common types with sensible defaults
            if (type == typeof(int))
            {
                // Example: Test with a set of typical integer values
                return new object[] { -1, 0, 1, int.MaxValue, int.MinValue };
            }
            else if (type == typeof(string))
            {
                // Example: Test with typical string values
                return new object[] { null, string.Empty, "example" };
            }
            else if (type == typeof(bool))
            {
                // Example: Test with both true and false for boolean
                return new object[] { true, false };
            }
            else if (type.IsEnum)
            {
                // Example: Test with all enum values for enumeration types
                return Enum.GetValues(type).Cast<object>();
            }
            // Add more types as needed...

            // For unknown types, return a default instance if possible
            if (type.IsValueType)
            {
                return new object[] { Activator.CreateInstance(type) };
            }
            else if (type.IsClass)
            {
                // For reference types, return null and a default instance
                return new object[] { null, Activator.CreateInstance(type) };
            }

            // If the type is not handled, return an empty array or throw an exception
            return new object[] { };
        }

        // Implement GetDefaultValuesForType similar to the previous example
    }
}
