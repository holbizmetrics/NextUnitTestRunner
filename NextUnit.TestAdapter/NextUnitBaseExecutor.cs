#define ADAPTER_TEST

using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System.Reflection;

using NextUnitTestResult = NextUnit.Core.TestResult;
using NextUnit.TestRunner.TestRunners.NewFolder;
namespace NextUnit.TestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)]
    public abstract class NextUnitBaseExecutor : IRunContext
    {
        protected ITestRunner5 TestRunner { get; set; } = new TestRunner5();
        public Type[] Types { get; set; } = null;

        #region IRunContext Interface
        public virtual bool KeepAlive => false;

        public virtual bool InIsolation => true;

        public virtual bool IsDataCollectionEnabled => true;

        public virtual bool IsBeingDebugged => Debugger.IsAttached;

        public virtual string? TestRunDirectory => HardwareContext.SystemInformation.SystemInformation.SystemInformation.KnownFolders.PublicPictures;

        public virtual string? SolutionDirectory => throw new NotImplementedException();

        public virtual IRunSettings? RunSettings => new TestAdapterTestRunSettings("DefaultTestRunSettings");

        #endregion IRunContext Interface
        public NextUnitBaseExecutor()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCase"></param>
        /// <returns></returns>
        protected virtual NextUnitTestResult ExecuteTest(TestCase testCase)
        {
#if ADAPTER_TEST
            Debugger.Launch();
            Debugger.Break();
#endif
            List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

            IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> classTestMethodsAssociation = null;
            if (Types == null)
            {
                Assembly assembly = Assembly.LoadFrom(testCase.Source);
                classTestMethodsAssociation = TestRunner.TestDiscoverer.Discover(assembly.GetTypes());

                foreach (var classTestMethodAssociation in classTestMethodsAssociation)
                {
                    Type definitionType = classTestMethodAssociation.Type;
                    if (TestRunner.InstanceCreationBehavior.OnlyInitializeAtStartBehavior) TestRunner.InstanceCreationBehavior.CreateInstance(definitionType);
                }
            }
            Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> classTestMethods = TestRunner.TestDiscoverer.CreateTestDelegates(classTestMethodsAssociation, TestRunner.InstanceCreationBehavior);

            string fullNameToMatch = $"{testCase.FullyQualifiedName}";
            IEnumerable<KeyValuePair<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)>> methodFoundByFullName =
                classTestMethods.Where(x => $"{x.Value.type.Namespace}.{x.Value.type.Name}.{x.Value.methodInfo.Name}" == fullNameToMatch);

            var methodToExecute = methodFoundByFullName.First(); // After ensuring there's at least one match.

            NextUnitTestResult nextUnitTestResult = TestRunner.ExecuteTest(methodToExecute);
            return nextUnitTestResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supportedProperties"></param>
        /// <param name="propertyProvider"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ITestCaseFilterExpression? GetTestCaseFilter(IEnumerable<string>? supportedProperties, Func<string, TestProperty?> propertyProvider)
        {
            throw new NotImplementedException();
        }       
    }
}

