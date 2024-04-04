using System.Diagnostics;
using System.Reflection;
using NextUnit.Core;
using NextUnit.Core.Asserts;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using NextUnit.TestEnvironment;

namespace NextUnit.TestRunner.TestRunners
{
    public interface ITestRunner
    {
        void Run(Type type);
        void Run(string name, params Type[] types);
        void Run(object objectToGetTypeFrom);
        event ExecutionEventHandler BeforeTestRun;
        ITestDiscoverer TestDiscoverer { get; set; }

        /// <summary>
        /// This event will be fired after each test run.
        /// </summary>
        event ExecutionEventHandler AfterTestRun;

        /// <summary>
        /// This will be fired for each test being executed.
        /// </summary>
        event ExecutionEventHandler TestExecuting;

        /// <summary>
        /// This will be fired if the complete test run is started.
        /// </summary>
        event ExecutionEventHandler TestRunStarted;

        /// <summary>
        /// This will be fired if the complete test run is ended.
        /// </summary>
        event ExecutionEventHandler TestRunFinished;

        /// <summary>
        /// This will be fired if an error occurs during the test run.
        /// </summary>
        event ExecutionEventHandler ErrorEventHandler;
    }

    public interface ITestRunner3 : ITestRunner
    {
        bool UseThreading { get; set; }
        IAttributeLogicMapper AttributeLogicMapper { get; set; }
        bool UseCombinator { get; set; }
        bool RecreateClassObject { get; }
        void Dispose();
        TestResult ExecuteTest(MethodInfo methodInfo, object classInstance);
    }

    /// <summary>
    /// This is just a first TestRunner as a proof of concept.
    /// </summary>
    [Obsolete("This TestRunner may not work anymore.")]
    public class TestRunner : ITestRunner
    {
        public IEnumerable<(Type Type, MethodInfo Method, IEnumerable<TestAttribute> Attributes)> ClassTestMethodsAssociation = null;
        public ITestDiscoverer TestDiscoverer { get; set; } = new TestDiscoverer();

        public virtual event ExecutionEventHandler BeforeTestRun;
        public virtual event ExecutionEventHandler AfterTestRun;
        public virtual event ExecutionEventHandler TestExecuting;
        public virtual event ExecutionEventHandler TestRunStarted;
        public virtual event ExecutionEventHandler TestRunFinished;

        public event ExecutionEventHandler ErrorEventHandler;

        /// <summary>
        /// Only here to fulfill the interface for now.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Run(string name, params Type[] types)
        {
            throw new NotImplementedException();
        }

        public virtual void Run(Type type = null)
        {
            Type[] types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            Type[] classes = types.Where(t => t.IsClass && !t.IsAbstract).ToArray();

            string machineName = System.Environment.MachineName;

            // Show Hardware Snapshots
            Trace.WriteLine("------------------");
            Trace.WriteLine("Hardware snapshot:");
            Trace.WriteLine("------------------");
            Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
            Trace.WriteLine("");

            foreach (Type testClass in classes)
            {
                //Since we've already went through for a type we only have to create an object once.
                List<MethodInfo> methodInfos = TestDiscoverer.Discover(testClass);
                if (methodInfos.Count == 0) continue;
                object classObject = Activator.CreateInstance(testClass);
                foreach (MethodInfo method in methodInfos)
                {
                    object[] parameters = null;
                    IEnumerable<Attribute> attributes = method.GetCustomAttributes();
                    if (attributes.Any(x => x.GetType() == typeof(TestAttribute) || x.GetType().BaseType == typeof(TestAttribute)))
                    {
                        int executionCount = 1;
                        foreach (Attribute attribute in attributes)
                        {
                            parameters = MethodAttributeInterpreter.Interpret(attribute as CommonTestAttribute);
                            if (parameters != null)
                            {
                                executionCount = attribute.GetType().GetValue<int>("ExecutionCount", attribute);
                            }

                            Exception lastException = null;
                            TestResult testResult = TestResult.Empty;
                            try
                            {
                                for (int i = 0; i < executionCount; i++)
                                {
                                    testResult = new TestResult();
                                    testResult.Namespace = method.DeclaringType.ToString();
                                    testResult.Workstation = machineName;
                                    testResult.DisplayName = method.Name;

                                    Stopwatch stopwatch = Stopwatch.StartNew();
                                    testResult.Start = DateTime.Now;
                                    method.Invoke(classObject, parameters);
                                    stopwatch.Stop();

                                    testResult.State = ExecutionState.Passed;
                                    testResult.ExecutionTime = stopwatch.Elapsed;
                                    testResult.End = DateTime.Now;
                                    Trace.WriteLine(testResult.ToString());
                                    Trace.WriteLine("");
                                }
                            }
                            catch (AssertException ex)
                            {
                                lastException = ex;
                                Trace.WriteLine(ex);
                            }
                            catch (TargetInvocationException ex)
                            {
                                lastException = ex;
                                if (ex.InnerException != null)
                                {
                                    Trace.WriteLine(ex.InnerException);
                                }
                                else
                                {
                                    Trace.WriteLine(ex);
                                }
                            }
                            catch (TargetParameterCountException ex)
                            {
                                lastException = ex;
                                Trace.WriteLine(ex);
                            }
                            catch (Exception ex)
                            {
                                lastException = ex;
                                Trace.WriteLine(ex);
                            }
                            finally
                            {
                                if (lastException != null)
                                {
                                    testResult.StackTrace = lastException?.StackTrace;
                                }
                                if (testResult == null)
                                {
                                    testResult = new TestResult();
                                }
                                testResult.End = DateTime.Now;
                            }
                        }
                    }
                }

                //Show hardware snapshot.
                Trace.WriteLine("Hardware snapshot:");
                Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
                Trace.WriteLine("");
            }
        }

        public virtual void Run(object objectToGetTypeFrom)
        {
            Run(objectToGetTypeFrom.GetType());
        }

        public virtual IEnumerable<(Type Type, MethodInfo Method, IEnumerable<TestAttribute> Attributes)> ExecutedMethodsPerClass
        {
            get { return ClassTestMethodsAssociation; }
        }
    }
}
