using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestClasses;
using System.Reflection;

namespace NextUnit.TestRunner
{
    public interface ITestDiscoverer
    {
        List<MethodInfo> Discover(Type testClass);
    }

    /// <summary>
    /// Discovers all the tests for a specific type.
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

        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            // Example: Reflectively inspect assemblies and find tests with GroupAttribute
            foreach (var source in sources)
            {
                var assembly = Assembly.LoadFrom(source);
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var method in type.GetMethods())
                    {
                        var groupAttribute = method.GetCustomAttribute<GroupAttribute>();
                        if (groupAttribute != null)
                        {
                            var testCase = new TestCase(/* ... */);
                            testCase.Traits.Add("Group", groupAttribute.GroupName);
                            discoverySink.SendTestCase(testCase);
                        }
                    }
                }
            }
        }
    }
}
