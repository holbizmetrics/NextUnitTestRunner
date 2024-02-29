using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class PermutationAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var parameterInfos = testMethod.GetParameters();
            if (parameterInfos.Length == 0)
            {
                // Invoke the method directly if it has no parameters
                testMethod.Invoke(testInstance, @delegate, null);
                return;
            }

            // Here we need to generate all permutations of parameters.
            // This is a complex task, depending on the types and number of parameters.
            // The logic here should create an IEnumerable<IEnumerable<object>> where
            // each inner IEnumerable<object> represents a set of parameters for a single test invocation.

            var allParameterCombinations = GenerateParameterCombinations(parameterInfos);

            foreach (var parameterSet in allParameterCombinations)
            {
                testMethod.Invoke(testInstance, @delegate, parameterSet.ToArray());
            }
        }

        private IEnumerable<IEnumerable<object>> GenerateParameterCombinations(ParameterInfo[] parameterInfos)
        {
            // This method should generate all permutations of parameter values.
            // This is a non-trivial task and would need custom implementation based
            // on how you want to define the values for each parameter type.

            // Example: For simplicity, let's assume each parameter is an integer,
            // and we want to test all combinations of values 0, 1, and 2 for each parameter.
            // You would need to replace this with logic appropriate for your parameters.

            var values = new List<int> { 0, 1, 2 };
            return GetPermutations(values, parameterInfos.Length);
        }

        private IEnumerable<IEnumerable<object>> GetPermutations(List<int> values, int length)
        {
            var results = new List<List<object>>();

            if (length == 1)
            {
                // If the length is 1, return each value as a single-item permutation
                return values.Select(v => (IEnumerable<object>)new List<object> { v });
            }

            // Get permutations of length (n-1)
            var subPermutations = GetPermutations(values, length - 1);

            foreach (var subPermutation in subPermutations)
            {
                foreach (var value in values)
                {
                    var newPermutation = new List<object>(subPermutation) { value };
                    results.Add(newPermutation);
                }
            }

            return results;
        }
    }


}
