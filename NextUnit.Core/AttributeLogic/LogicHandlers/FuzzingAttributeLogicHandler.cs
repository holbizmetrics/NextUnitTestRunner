using NextUnit.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class FuzzingAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
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
                try
                {
                    testMethod.Invoke(testInstance, combination.ToArray());
                }
                catch (Exception ex)
                {
                    // Handle exceptions or record failures as needed
                }
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
                else if (parameterType == typeof(string))
                {
                    return new object[] { string.Empty, null, "test", Guid.NewGuid().ToString() };
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