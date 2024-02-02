#define ADAPTER_TEST

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace NextUnitTestAdapter
{
    [ExtensionUri(DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    [DefaultExecutorUri(DiscovererURI)]//[DefaultExecutorUri("executor://NextUnitTestDiscoverer")]
    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [Category("managed")]
    public class NextUnitTestDiscoverer : ITestDiscoverer
    {
        public const string DiscovererURI = "executor://NextUnitTestDiscoverer";
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
            logger.SendMessage(TestMessageLevel.Error, "Discovering Tests:");
            foreach (string source in sources)
            {
                // Example: Load the assembly and discover test methods
                var assembly = Assembly.LoadFrom(source);
                IEnumerable<(Type Type, MethodInfo Method, IEnumerable<CommonTestAttribute> Attributes)> TestMethodsPerClass = ReflectionExtensions.GetMethodsWithAttributesAsIEnumerableGeneric<CommonTestAttribute>(assembly.GetTypes());

                //so we should be able to execute here, already.
                foreach (var testDefinition in TestMethodsPerClass)
                {
                    (Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition = ((Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes))testDefinition;
                    Type definitionType = definition.type;
                    MethodInfo method = definition.methodInfo;
                    // Create a TestCase from the discovered test method
                    var testCase = new TestCase(method.Name, new Uri("executor://NextUnitTestDiscoverer"), source);

                    GroupAttribute groupAttribute = method.GetCustomAttribute<GroupAttribute>();
                    if (groupAttribute != null)
                    {
                        string groupName = null;
                        if (groupAttribute.AutomaticNaming)
                        {
                            //TODO: implement mechanisms here to generate a group name automatically.
                            //if it can be done.
                        }
                        else
                        {
                            groupName = groupAttribute.GroupName;
                        }
                        testCase.Traits.Add(new Trait(groupName, string.IsNullOrEmpty(groupAttribute.Value) ? groupName : groupName));
                    }
                    // Add the test case to the discovery sink
                    discoverySink.SendTestCase(testCase);
                }
            }
        }

        // Method to discover tests in an assembly (simplified for example purposes)
        private IEnumerable<MethodInfo> DiscoverTestsInAssembly(Assembly assembly)
        {
            var testMethods = new List<MethodInfo>();

            foreach (Type type in assembly.GetTypes())
            {
                // Only consider public instance methods
                foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                {
                    // Check if the method is marked with the TestAttribute (or your custom test attribute)
                    var isTestMethod = method.GetCustomAttributes(typeof(TestAttribute), inherit: false).Any();

                    if (isTestMethod)
                    {
                        testMethods.Add(method);
                    }
                }
            }

            return testMethods;
        }
    }
}
