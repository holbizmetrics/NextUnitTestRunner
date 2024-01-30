using System.Diagnostics;
using System.Reflection;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.Assertions;

namespace NextUnit.TestRunner
{
    public interface ITestRunner
    {
        void Run(Type type);
        void Run(string name, params Type[] types);
        void Run(object objectToGetTypeFrom);
        event ExecutionEventHandler BeforeTestRun;

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
        AttributeLogicMapper AttributeLogicMapper { get; set; }
        bool UseCombinator { get; set; }
    }

    /// <summary>
    /// This is just a first TestRunner as a proof of concept.
    /// </summary>
    public class TestRunner : ITestRunner
    {
        public Dictionary<Type, List<MethodInfo>> ClassTestMethodsAssociation = new Dictionary<Type, List<MethodInfo>>();
        protected TestDiscoverer discoverer = new TestDiscoverer();

        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;
        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
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

            string machineName = Environment.MachineName;

            // Show Hardware Snapshots
            Trace.WriteLine("Hardware snapshot:");
            Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
            Trace.WriteLine("");

            foreach (Type testClass in classes)
            {
                //Since we've already went through for a type we only have to create an object once.
                List<MethodInfo> methodInfos = discoverer.Discover(testClass);
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
                            TestResult testResult = null;
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

                                    testResult.State = ExecutedState.Passed;
                                    testResult.ExecutionTime = stopwatch.Elapsed;
                                    testResult.End = DateTime.Now;
                                    Trace.WriteLine(testResult.ToString());
                                    Trace.WriteLine("");                                    
                                }
                            }
                            catch (AssertException ex)
                            {
                                lastException = ex;
                                Trace.WriteLine(ex.Message);
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
                                if (testResult == null)
                                {
                                    testResult =  new TestResult();
                                }
                                testResult.End = DateTime.Now;
                                testResult.StackTrace = lastException?.StackTrace;
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

        public void Run(object objectToGetTypeFrom)
        {
            Run(objectToGetTypeFrom.GetType());
        }

        public Dictionary<Type, List<MethodInfo>> ExecutedMethodsPerClass
        {
            get { return this.ClassTestMethodsAssociation; }
        }
    }
}
