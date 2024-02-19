using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
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
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public Type[] DiscoverTests(Type[] types, Dictionary<Type, List<MethodInfo>> classTestMethodsAssociation)
        {
            classTestMethodsAssociation.Clear();
            if (types != null && types.Length == 1)
            {
                Type type = types[0];
                types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            }
            Type[] classes = types.Where(t => t.IsClass).ToArray();

            string machineName = Environment.MachineName;

            foreach (Type testClass in classes)
            {
                List<MethodInfo> methodInfos = Discover(testClass);
                if (methodInfos.Count > 0)
                {
                    classTestMethodsAssociation.Add(testClass, methodInfos);
                }
            }

            return types;
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
