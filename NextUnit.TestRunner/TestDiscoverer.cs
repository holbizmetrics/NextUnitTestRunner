using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.TestRunner
{
    public interface ITestDiscoverer
    {
        List<MethodInfo> Discover(Type testClass);
        IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> Discover(params Type[] types);
    }

    /// <summary>
    /// Discovers all the tests for a specific type.
    /// </summary>
    public class TestDiscoverer : ITestDiscoverer
    {
        public IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> Discover(params Type[] types)
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
    }
}
