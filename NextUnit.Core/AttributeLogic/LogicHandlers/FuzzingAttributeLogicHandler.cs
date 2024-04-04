using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class FuzzingAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var testDataGenerator = new TestDataGenerator();
            var parameterInfos = testMethod.GetParameters();
            var allParameterValues = new List<IEnumerable<object>>();

            // Generate test data for each parameter
            foreach (var parameter in parameterInfos)
            {
                var testData = testDataGenerator.GenerateTestData(parameter.ParameterType);
                allParameterValues.Add(testData);
            }

            // Generate all possible combinations of parameter values
            foreach (var combination in GenerateParameterCombinations(allParameterValues, 0))
            {
                //try
                //{
                    testMethod.Invoke(testInstance, @delegate, combination.ToArray());
                //}
                //catch (Exception ex)
                //{
                    // Handle exceptions or record failures as needed
                //}
            }
        }

        // Generates all combinations of parameter values
        private IEnumerable<IEnumerable<object>> GenerateParameterCombinations(List<IEnumerable<object>> allParameterValues, int parameterIndex)
        {
            if (parameterIndex == allParameterValues.Count)
            {
                yield return new object[0];
                yield break;
            }

            foreach (var item in allParameterValues[parameterIndex])
            {
                foreach (var restCombination in GenerateParameterCombinations(allParameterValues, parameterIndex + 1))
                {
                    yield return new[] { item }.Concat(restCombination);
                }
            }
        }

        public class TestDataGenerator
        {
            private Random _random = new Random();

            public IEnumerable<object> GenerateTestData(Type parameterType)
            {
                // Adapted to generate a sequence of test data values for a single parameter
                if (parameterType == typeof(int))
                {
                    return new object[] { 0, 1, -1, _random.Next(), int.MaxValue, int.MinValue };
                }
                else if (parameterType == typeof(double))
                {
                    return new object[] { 0d, 1d, -1d, _random.Next(), double.MaxValue, double.MinValue };
                }
                else if (parameterType == typeof(float))
                {
                    return new object[] { 0f, 1f, -1f, _random.Next(), float.MaxValue, float.MinValue };
                }
                else if (parameterType == typeof(decimal))
                {
                    return new object[] { 0m, 1m, -1m, _random.Next(), decimal.MaxValue, decimal.MinValue };
                }
                else if (parameterType == typeof(bool))
                {
                    return new object[] { true, false, true, false, false, false, true };
                }
                else if (parameterType == typeof(string))
                {
                    return new object[] { string.Empty, null, "test", Guid.NewGuid().ToString() };
                }
                else if (parameterType == typeof(TimeSpan))
                {
                    return new object[] { TimeSpan.FromSeconds(7), TimeSpan.FromMilliseconds(-1000), TimeSpan.FromHours(536), TimeSpan.FromHours(-3980), TimeSpan.Zero, TimeSpan.MinValue ,TimeSpan.MaxValue };
                }
                // Extend to other types as needed
                else if (parameterType.IsValueType || parameterType == typeof(object))
                {
                    // For value types and object, create a default instance
                    return new object[] { Activator.CreateInstance(parameterType) };
                }
                return new object[] { null }; // Fallback for reference types
            }
        }
    }
}