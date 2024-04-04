using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.TestRunner
{
    public interface ITestDiscoverer
    {
        List<MethodInfo> Discover(Type testClass);
        IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> Discover(params Type[] types);
        Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> CreateTestDelegates(IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> testMethodsPerClass, IInstanceCreationBehavior instanceCreationBehavior);
    }

    /// <summary>
    /// Discovers all the tests for a specific type.
    /// </summary>
    public class TestDiscoverer : ITestDiscoverer
    {
        public virtual IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> Discover(params Type[] types)
        {
            IEnumerable <(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> testMethodsPerClass = ReflectionExtensions.GetMethodsWithAttributesAsIEnumerableGeneric2<Attribute>(types);
            return testMethodsPerClass;
        }

        [Obsolete]
        public List<MethodInfo> Discover(Type testClass)
        {
            List<MethodInfo> discoveredValidTestMethods = new List<MethodInfo>();
            MethodInfo[] methodInfos = testClass.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo method in methodInfos)
            {
                IEnumerable<Attribute> attribute = method.GetCustomAttributes();

                if (attribute.Any(x => x.GetType() == typeof(TestAttribute) || x.GetType().BaseType == typeof(TestAttribute)))
                {
                    discoveredValidTestMethods.Add(method);
                }
            }
            return discoveredValidTestMethods;
        }

        /// <summary>
        /// Discovers creates a delegate list from discovered tests
        /// This may be part of the testdiscoverer or still stay in the testrunner. Do not know, yet.
        /// </summary>
        public virtual Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> CreateTestDelegates(IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> testMethodsPerClass, IInstanceCreationBehavior instanceCreationBehavior)
        {
            Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> testMethodDelegates = new Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)>();
            foreach (var testDefinition in testMethodsPerClass)
            {
                testMethodDelegates.Add(
                    $"{testDefinition.Method.DeclaringType.FullName}.{testDefinition.Method.Name}",
                    (testDefinition.Type, testDefinition.Method, testDefinition.Attributes,
                        testDefinition.Method.CreateTestDelegate(instanceCreationBehavior.CreateInstance(testDefinition.Type))));
            }
            return testMethodDelegates;
        }
    }
}
