using System.Diagnostics;
using System.Reflection;
using NextUnitTestRunner.Assertions;
using NextUnitTestRunner.Extensions;
using NextUnit.Core.TestAttributes;

namespace NextUnitTestRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionEventArgs
    {
        public MethodInfo MethodInfo { get; set; } = null;
        public TestResult TestResult { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ExecutionEventArgs()
        {
        }

        public ExecutionEventArgs(MethodInfo methodInfo, TestResult testResult)
            : this(methodInfo)
        {
            TestResult = testResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public ExecutionEventArgs(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }
    }

    public delegate void ExecutionEventHandler(object sender, ExecutionEventArgs e);

    /// <summary>
    /// A little bit further progressed TestRunner.
    /// For sure nowhere near where it should be, but already way better like this than the first one.
    /// 
    /// This TestRunner will support events (before, after, executing).
    /// And an own TestDiscoverer can be injected.
    /// 
    /// As well as this one supports execution in threads rudimentarily.
    /// </summary>
    public class TestRunner2 : TestRunner, ITestRunner
    {
        public ITestDiscoverer TestDiscoverer { get; set; } = new TestDiscoverer();
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;

        protected Dictionary<int, MethodInfo> classTypeMethodInfosAssociation { get; } = new Dictionary<int, MethodInfo>();
        public bool UseThreading { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnBeforeTestRun(ExecutionEventArgs e)
        {
            BeforeTestRun?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnAfterTestRun(ExecutionEventArgs e)
        {
            AfterTestRun?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestExecuting(ExecutionEventArgs e)
        {
            TestExecuting?.Invoke(this, e);
        }

        protected void OnTestRunStarted(ExecutionEventArgs e)
        {
            TestRunStarted?.Invoke(this, e);
        }
        protected void OnTestRunFinished(ExecutionEventArgs e)
        {
            TestRunFinished?.Invoke(this, e);
        }

        protected void OnError(ExecutionEventArgs e)
        {
            ErrorEventHandler?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void Run(Type type = null)
        {
            OnTestRunStarted(new ExecutionEventArgs());

            NextUnitTestExecutionContext.TestRunStart = DateTime.Now;
            Type[] types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            Type[] classes = types.Where(t => t.IsClass).ToArray();

            string machineName = Environment.MachineName;

            Dictionary<Type, List<MethodInfo>> classTestMethodsAssociation = new Dictionary<Type, List<MethodInfo>>();
            foreach (Type testClass in classes)
            {
                List<MethodInfo> methodInfos = TestDiscoverer.Discover(testClass);
                if (methodInfos.Count > 0)
                {
                    classTestMethodsAssociation.Add(testClass, methodInfos);
                }
            }

            if (UseThreading)
            {
                Thread thread = new Thread(() =>
                {
                    ExecuteTests(classTestMethodsAssociation);
                });
                thread.Start();
                thread.Join();
            }
            else
            {
                ExecuteTests(classTestMethodsAssociation);
            }

            OnTestRunFinished(new ExecutionEventArgs());
        }

        public void ExecuteTests(Dictionary<Type, List<MethodInfo>> classTestMethodsAssociation)
        {
            string machineName = NextUnitTestEnvironmentContext.MachineName;
            foreach (Type testClass in classTestMethodsAssociation.Keys)
            {
                List<MethodInfo> methodInfos = classTestMethodsAssociation[testClass];

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
                                    OnBeforeTestRun(new ExecutionEventArgs(method));
                                    testResult = new TestResult();
                                    testResult.Namespace = method.DeclaringType.ToString();
                                    testResult.Class = method.DeclaringType.Name;

                                    //theoretically AND practically for sure as well tests could be executed on
                                    //different machines in one test run.
                                    //But for now, for simplicity and the early version we'll leave it at only one time getting the name.
                                    testResult.Workstation = machineName;
                                    testResult.DisplayName = method.Name;

                                    Stopwatch stopwatch = Stopwatch.StartNew();
                                    OnTestExecuting(new ExecutionEventArgs(method));
                                    testResult.Start = DateTime.Now;
                                    method.Invoke(classObject, parameters);
                                    stopwatch.Stop();

                                    testResult.State = ExecutedState.Passed;
                                    testResult.ExecutionTime = stopwatch.Elapsed;
                                    testResult.End = DateTime.Now;

                                    NextUnitTestExecutionContext.TestResults.Add(testResult);
                                    OnAfterTestRun(new ExecutionEventArgs(method, testResult));
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
                                    testResult = new TestResult();
                                }
                                testResult.End = DateTime.Now;
                                testResult.StackTrace = lastException?.StackTrace;
                                NextUnitTestExecutionContext.TestResults.Add(testResult);

                                OnAfterTestRun(new ExecutionEventArgs(method, testResult));
                                if (lastException != null)
                                {
                                    OnError(new ExecutionEventArgs(method, testResult));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
