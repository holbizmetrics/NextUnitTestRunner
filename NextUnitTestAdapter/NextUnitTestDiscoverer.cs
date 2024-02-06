#define ADAPTER_TEST

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using NextUnit.TestAdapter;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;

namespace NextUnitTestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)] //[ExtensionUri("executor://NextUnitTestDiscoverer")]
    [DefaultExecutorUri(Definitions.DiscovererURI)]//[DefaultExecutorUri("executor://NextUnitTestDiscoverer")]
    [FileExtension(Definitions.dll)] //[FileExtension(".dll")]
    [FileExtension(Definitions.exe)] //[FileExtension(".exe")]
    [Category(Definitions.managed)]//[Category("managed")]
    public class NextUnitTestDiscoverer : ITestDiscoverer
    {        
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

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
                    var testCase = new TestCase(method.Name, new Uri(Definitions.DiscovererURI), source); //new TestCase(method.Name, new Uri("executor://NextUnitTestDiscoverer"), source);
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

        public void GetSourceCodeLine()
        {
            //var assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyPath, readerParameters);

            //foreach (var module in assemblyDefinition.Modules)
            //{
            //    foreach (var type in module.Types)
            //    {
            //        foreach (var method in type.Methods)
            //        {
            //            if (method.HasBody && method.DebugInformation.HasSequencePoints)
            //            {
            //                foreach (var seqPoint in method.DebugInformation.SequencePoints)
            //                {
            //                    Console.WriteLine($"Method: {method.FullName}");
            //                    Console.WriteLine($"File: {seqPoint.Document.Url}");
            //                    Console.WriteLine($"Start Line: {seqPoint.StartLine}");
            //                    Console.WriteLine($"End Line: {seqPoint.EndLine}");
            //                    // Break or continue as needed
            //                }
            //            }
            //        }
            //    }
            //}
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
