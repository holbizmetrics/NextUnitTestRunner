using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;
using System.Diagnostics;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RunAllDelegatePermutationsLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            RunAllDelegatePermutationsAttribute runAllDelegateCombinations = attribute as RunAllDelegatePermutationsAttribute;
            Action[] testMethods = GetDelegatesFromActionDelegateNames(runAllDelegateCombinations.Actions, testInstance).ToArray();

            if (testMethods != null && testMethods.Length > 0)
            {
                RunAllCombinations(testMethods);
            }
            // we might be wanna throwing an error, here?
            //else
            //{

            //}
        }

        public List<Action> GetDelegatesFromActionDelegateNames(string[] names, object objectInstance)
        {
            Type type = objectInstance.GetType();
            MethodInfo[] methodInfos = type.GetMethods(names);
            List<Action> result = new List<Action>();

            foreach (MethodInfo methodInfo in methodInfos)
            {
                // Ensure the method is compatible with Action delegate (no parameters and void return type)
                if (methodInfo.ReturnType == typeof(void) && !methodInfo.GetParameters().Any())
                {
                    Action actionDelegate;
                    if (methodInfo.IsStatic)
                    {
                        // Create delegate for static method
                        actionDelegate = (Action)Delegate.CreateDelegate(typeof(Action), null, methodInfo);
                    }
                    else
                    {
                        // Create delegate for instance method
                        actionDelegate = (Action)Delegate.CreateDelegate(typeof(Action), objectInstance, methodInfo);
                    }
                    result.Add(actionDelegate);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testMethods"></param>
        public void RunAllCombinations(Action[] testMethods)
        {
            var permutations = Permute(testMethods);

            foreach (var permutation in permutations)
            {
                foreach (var testMethod in permutation)
                {
                    testMethod();
                }
                Trace.WriteLine("----");
            }
        }

        private IEnumerable<IEnumerable<Action>> Permute(Action[] testMethods)
        {
            int n = testMethods.Length;
            int[] indices = new int[n];
            for (int i = 0; i < n; i++)
                indices[i] = i;

            yield return GetPermutation(testMethods, indices);

            while (true)
            {
                int i = n - 1;
                while (i > 0 && indices[i - 1] >= indices[i])
                    i--;

                if (i <= 0)
                    yield break;

                int j = n - 1;
                while (indices[j] <= indices[i - 1])
                    j--;

                (indices[i - 1], indices[j]) = (indices[j], indices[i - 1]);

                for (j = n - 1; i < j; i++, j--)
                    (indices[i], indices[j]) = (indices[j], indices[i]);

                yield return GetPermutation(testMethods, indices);
            }
        }

        private IEnumerable<Action> GetPermutation(Action[] testMethods, int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                yield return testMethods[indices[i]];
            }
        }
    }


}
