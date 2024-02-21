#if ADAPTER_TEST
using System.Diagnostics;
#endif

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnit.TestRunner.TestRunners;
using System.Diagnostics;
using System.Reflection;

using NextUnitTestResult = NextUnit.Core.TestResult;
using TestResult = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult;
namespace NextUnit.TestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)]
    public abstract class NextUnitBaseExecutor : IRunContext
    {
        protected ITestRunner4 TestRunner { get; set; } = new TestRunner4();
        public Type[] Types { get; set; } = null;

        #region IRunContext Interface
        public virtual bool KeepAlive => false;

        public virtual bool InIsolation => true;

        public virtual bool IsDataCollectionEnabled => throw new NotImplementedException();

        public virtual bool IsBeingDebugged => Debugger.IsAttached;

        public virtual string? TestRunDirectory => throw new NotImplementedException();

        public virtual string? SolutionDirectory => throw new NotImplementedException();

        public virtual IRunSettings? RunSettings => throw new NotImplementedException();

        #endregion IRunContext Interface
        public NextUnitBaseExecutor()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCase"></param>
        /// <returns></returns>
        protected virtual TestResult ExecuteTest(TestCase testCase)
        {
#if ADAPTER_TEST
            Debugger.Launch();
#endif
            List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

            IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> classTestMethodsAssociation = null;
            if (Types == null)
            {
                Assembly assembly = Assembly.LoadFrom(testCase.Source);
                classTestMethodsAssociation = TestRunner.TestDiscoverer.Discover(assembly.GetTypes());

                foreach (var classTestMethodAssociation in classTestMethodsAssociation)
                {
                    (Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition = classTestMethodAssociation;

                    Type definitionType = definition.type;
                    if (TestRunner.InstanceCreationBehavior.OnlyInitializeAtStartBehavior) TestRunner.InstanceCreationBehavior.CreateInstance(definitionType);
                }
            }

            string fullNameToMatch = $"{testCase.FullyQualifiedName}";
            IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> methodFoundByFullName =
                classTestMethodsAssociation
                .Where(x => $"{x.Type.Namespace}.{x.Type.Name}.{x.Method.Name}" == fullNameToMatch);

            var methodToExecute = methodFoundByFullName.First(); // After ensuring there's at least one match.
            NextUnitTestResult nextUnitTestResult = TestRunner.ExecuteTest(methodToExecute);
            return testCase.ConvertTestCase(nextUnitTestResult);
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

