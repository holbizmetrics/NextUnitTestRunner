using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnit.Core.Extensions;
using NextUnit.TestRunner;
using NextUnit.TestRunner.TestClasses;
using System.Diagnostics;
using System.Reflection;

using NextUnitTestResult = NextUnit.TestRunner.TestResult;
using TestResult = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult;
namespace NextUnit.TestAdapter
{
    [ExtensionUri(Definitions.DiscovererURI)]
    public abstract class NextUnitBaseExecutor : IRunContext
    {
        protected TestRunner3 TestRunner = new TestRunner3();
        public Type[] Types { get; set; } = null;

        public Dictionary<Type, object> Properties { get; set; } = new Dictionary<Type, object>();

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
                    if (!Properties.ContainsKey(definitionType))
                    {
                        Properties.Add(definitionType, Activator.CreateInstance(definitionType));
                    }
                }
            }

            IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> methodFoundByName = ReflectionExtensions.FindMethodByName(classTestMethodsAssociation, testCase.DisplayName);
           
            (Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes) methodDefinition = methodFoundByName.First();

            NextUnitTestResult nextUnitTestResult = TestRunner.ExecuteTest(methodDefinition.Method, Properties[methodDefinition.Type]);
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

