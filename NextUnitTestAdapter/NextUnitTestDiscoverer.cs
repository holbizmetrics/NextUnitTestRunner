#define ADAPTER_TEST

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.Core.TestAttributes;
using NextUnit.TestAdapter;
using NextUnit.TestRunner;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace NextUnitTestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    [DefaultExecutorUri(Definitions.DiscovererURI)]//[DefaultExecutorUri("executor://NextUnitTestDiscoverer")]
    [FileExtension(Definitions.dll)] //[FileExtension(".dll")]
    [FileExtension(Definitions.exe)] //[FileExtension(".exe")]
    [Category(Definitions.managed)]//[Category("managed")]
    public class NextUnitTestDiscoverer : Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter.ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

#if ADAPTER_TEST
            Debugger.Launch();
#endif
            TestDiscoverer testDiscoverer = new TestDiscoverer();
            logger.SendMessage(TestMessageLevel.Error, "Discovering Tests:");
            foreach (string source in sources)
            {
                // Example: Load the assembly and discover test methods
                var assembly = Assembly.LoadFrom(source);
                IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> TestMethodsPerClass = testDiscoverer.Discover(assembly.GetTypes());

                //so we should be able to execute here, already.
                foreach (var testDefinition in TestMethodsPerClass)
                {
                    Type definitionType = testDefinition.Type;
                    MethodInfo method = testDefinition.Method;
                    // Create a TestCase from the discovered test method
                    var testCase = new TestCase(method.Name, new Uri(Definitions.DiscovererURI), source); //new TestCase(method.Name, new Uri("executor://NextUnitTestDiscoverer"), source);
                    string fullyQualifiedName = $"{definitionType.Namespace}.{definitionType.Name}.{method.Name}";
                    testCase.FullyQualifiedName = fullyQualifiedName;
                    testCase.CodeFilePath = Definitions.DiscovererURI;
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
    }
}
