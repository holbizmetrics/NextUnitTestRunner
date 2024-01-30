using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NextUnit.TestRunner;
using System.Reflection;

using NextUnit.TestRunner;
using NextUnitTestResult = NextUnit.TestRunner.TestResult;
using TestResult = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult;
using NextUnit.TestRunner.TestClasses;
using System.Reflection.Metadata;
namespace NextUnit.TestAdapter
{
    [ExtensionUri("executor://NextUnitTestDiscoverer")]
    public abstract class NextUnitBaseExecutor
    {
        protected TestRunner3 testRunner = new TestRunner3();
        public Type[] Types { get; set; } = null;

        public Dictionary<Type, object> Properties { get; set; } = new Dictionary<Type, object>();
        public NextUnitBaseExecutor()
        {
        }

        protected virtual TestResult ExecuteTest(TestCase testCase)
        {
            if (Types == null)
            {
                Assembly assembly = Assembly.LoadFrom(testCase.Source);
                Types = testRunner.DiscoverTests(assembly.GetTypes());

                foreach (Type testClass in testRunner.ClassTestMethodsAssociation.Keys)
                {
                    List<MethodInfo> methodInfos = testRunner.ClassTestMethodsAssociation[testClass];
                    if (methodInfos.Count == 0) continue;

                    object classObject = Activator.CreateInstance(testClass);
                    Properties.Add(testClass, classObject);
                }
            }
            (Type type, MethodInfo method) blub = FindMethodByName(testCase.DisplayName);
            NextUnitTestResult nextUnitTestResult = testRunner.ExecuteTest(blub.method, Properties[blub.type]);
            TestResult testResult = new TestResult(testCase);

            TestOutcome testOutcome = nextUnitTestResult.State switch
            {
                ExecutedState.Passed => testResult.Outcome = TestOutcome.Passed,
                ExecutedState.Failed => testResult.Outcome = TestOutcome.Failed,
                ExecutedState.Skipped => testResult.Outcome = TestOutcome.Skipped,
                ExecutedState.NotFound => testResult.Outcome = TestOutcome.NotFound,
            };
            testResult.Outcome = testOutcome;
            return testResult;
        }

        public (Type, MethodInfo) FindMethodByName(string methodName)
        {
            var result = testRunner.ClassTestMethodsAssociation
                .SelectMany(entry => entry.Value.Select(method => (Type: entry.Key, Method: method)))
                .FirstOrDefault(tuple => tuple.Method.Name == methodName);

            return (result.Type, result.Method);
        }
    }
}

