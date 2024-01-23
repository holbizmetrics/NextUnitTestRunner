using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnitTestRunner
{
    public interface ITestDiscoverer
    {
        List<MethodInfo> Discover(Type testClass);
    }

    /// <summary>
    /// Discoveres all the tests for a specific type.
    /// </summary>
    public class TestDiscoverer : ITestDiscoverer
    {
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
