using NextUnitTestRunner.TestAttributes;
using System.Reflection;

namespace NextUnitTestRunner
{
    /// <summary>
    /// Discoveres all the tests for a specific type.
    /// </summary>
    public class TestDiscoverer
    {
        public static List<MethodInfo> Discover(Type testClass)
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
