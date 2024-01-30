using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.Core.TestAttributes;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace NextUnitTestAdapter
{
    [ExtensionUri("executor://NextUnitTestDiscoverer")]
    [DefaultExecutorUri("executor://NextUnitTestDiscoverer")]
    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [Category("managed")]
    public class NextUnitTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            Debugger.Launch();
            logger.SendMessage(TestMessageLevel.Error, "DiscoverTests");
            foreach (string source in sources)
            {
                // Example: Load the assembly and discover test methods
                var assembly = Assembly.LoadFrom(source);
                foreach (MethodInfo test in DiscoverTestsInAssembly(assembly))
                {
                    // Create a TestCase from the discovered test method
                    var testCase = new TestCase(test.Name, new Uri("executor://NextUnitTestDiscoverer"), source);
                    //testCase.Traits.Add(new Trait("Blub", "1")); //It works if we put this in here but we need to scan for the GroupAttribute marked tests.

                    //testCase.CodeFilePath = 
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
