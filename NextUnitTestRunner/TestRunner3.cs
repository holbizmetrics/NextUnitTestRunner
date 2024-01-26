using System.Diagnostics;
using System.Reflection;
using NextUnit.TestRunner.Assertions;
using NextUnit.Core.TestAttributes;
using System.Runtime.Loader;
using NextUnit.Core.AttributeLogic;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// A little bit further progressed TestRunner.
    /// 
    /// Additionally added compared to TestRunner2:
    /// 
    /// The TestResults might have been added twice for one test method run, when certain conditions applied.
    /// 
    /// The attributes logic was only implemented in the test runner for the execution count using reflection.
    /// This has now been "outsorced" into an attribute handling mechanisms by the AttributeLogicHandler.
    /// So far this is provided by a dictionary. The handler will be taken out of the dictionary if available for the current attribute.
    /// Then the logic in the handler will be applied.
    /// 
    /// The user can be leveraging the eventhandler to implement own logic for documentation purposes, etc.
    /// 
    /// Before Test Suite Running the user may choose if all methods will be executed in different threads.
    /// If not, this will happen sequentially.
    /// 
    /// </summary>
    public class TestRunner3 : TestRunner, ITestRunner3
    {
        public ITestDiscoverer TestDiscoverer { get; set; } = new TestDiscoverer();
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;
        public AttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();

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

        public void Run(object objectToGetTypeFrom)
        {
            Run(objectToGetTypeFrom.GetType());
        }

        /// <summary>
        /// Runs the test.
        /// If there is an error occurring an error event will be triggered.
        /// </summary>
        /// <param name="name"></param>
        public void Run(string name)
        {
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            AssemblyLoadContext.Default.Unloading += Default_Unloading;
            AssemblyLoadContext.Default.ResolvingUnmanagedDll += Default_ResolvingUnmanagedDll;
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(name);

            Type[] types = assembly.GetTypes();
            Run(types);
        }

        /// <summary>
        /// Handle resolving of unmanaged DLLs here.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private nint Default_ResolvingUnmanagedDll(Assembly arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Use this when default unloading of an AssemblyLoadContext occurs.
        /// </summary>
        /// <param name="obj"></param>
        private void Default_Unloading(AssemblyLoadContext obj)
        {
            Trace.WriteLine($"Default unloading: {obj.ToString()}");
        }

        private Assembly? Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void Run(params Type[] types)
        {
            OnTestRunStarted(new ExecutionEventArgs());

            NextUnitTestExecutionContext.TestRunStart = DateTime.Now;

            if (types != null && types.Length == 1)
            {
                Type type = types[0];
                types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            }
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
                //thread.Join();
            }
            else
            {
                ExecuteTests(classTestMethodsAssociation);
            }

            OnTestRunFinished(new ExecutionEventArgs());
        }

        /// <summary>
        /// Executes the tests found by the TestDiscoverer.
        /// </summary>
        /// <param name="classTestMethodsAssociation"></param>
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
                            Exception lastException = null;
                            TestResult testResult = null;
                            try
                            {
                                OnBeforeTestRun(new ExecutionEventArgs(method));
                                testResult = new TestResult();
                                testResult.Namespace = method.DeclaringType.ToString();
                                testResult.Class = method.DeclaringType.Name;
                                testResult.Workstation = machineName;
                                testResult.DisplayName = method.Name;

                                Stopwatch stopwatch = Stopwatch.StartNew();
                                OnTestExecuting(new ExecutionEventArgs(method));
                                //Since we exclude the test marker attribute, a test logic handler should be found for each of the implemented framework attribute.
                                var handler = AttributeLogicMapper.GetHandlerFor(attribute);

                                handler?.ProcessAttribute(attribute, method, classObject);

                                testResult.Start = DateTime.Now;

                                testResult.State = ExecutedState.Passed;
                                stopwatch.Stop();

                                if (testResult.State != ExecutedState.Skipped)
                                {
                                    testResult.ExecutionTime = stopwatch.Elapsed;
                                    testResult.End = DateTime.Now;
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
                                    OnError(new ExecutionEventArgs(method, testResult, lastException));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
