using System.Diagnostics;
using System.Reflection;
using NextUnit.Core.TestAttributes;
using System.Runtime.Loader;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Asserts;
using NextUnit.Core;

namespace NextUnit.TestRunner.TestRunners
{
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
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;

        public ITestDiscoverer TestDiscoverer { get; set; } = new TestDiscoverer();
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

        private nint Default_ResolvingUnmanagedDll(Assembly arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        private void Default_Unloading(AssemblyLoadContext obj)
        {
            Trace.WriteLine($"Default unloading: {obj.ToString()}");
            obj.Unload();
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
                thread.Join();
            }
            else
            {
                ExecuteTests(classTestMethodsAssociation);
            }
            NextUnitTestExecutionContext.TestRunEnd = DateTime.Now;

            OnTestRunFinished(new ExecutionEventArgs());
        }

        public void ExecuteTests(Dictionary<Type, List<MethodInfo>> classTestMethodsAssociation)
        {
            AttributeLogicMapper attributeLogicMapper = new AttributeLogicMapper();
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
                            //Since we've already checked for that... not again, please.
                            if (attribute.GetType() == typeof(TestAttribute))
                            {
                                continue;
                            }

                            //Since we exclude the test marker attribute, a test logic handler should be found for each of the implemented framework attribute.
                            var handler = attributeLogicMapper.GetHandlerFor(attribute);
                            
                            //TODO: I guess we can throw away TestRunner2 when TestRunner5 is working.
                            handler?.ProcessAttribute(attribute, method, null, this);

                            //This has now to be accomplished with the help of the handler?.ProcessAttribute
                            //parameters = MethodAttributeInterpreter.Interpret(attribute as CommonTestAttribute);
                            //if (parameters != null)
                            //{
                            //    executionCount = attribute.GetType().GetValue<int>("ExecutionCount", attribute);
                            //}

                            Exception lastException = null;
                            TestResult testResult = TestResult.Empty;
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

                                    ParameterInfo[] parameterInfos = method.GetParameters();
                                    if (parameters == null && parameterInfos.Length > 0 || parameters != null && parameters.Length != method.GetParameters().Length && method.GetParameters().Length > 0)
                                    {
#if DEBUG
                                        Debug.WriteLine($"Parameter mismatch for method {method}. No parameters specified or the value is null.");
                                        if (parameters != null)
                                        {
                                            Debug.WriteLine($"Given: {parameters}, Expected: {parameterInfos}");
                                        }
#endif
                                        testResult.State = ExecutionState.Skipped;
                                        OnError(new ExecutionEventArgs(method, testResult));
                                    }
                                    else
                                    {
                                        method.Invoke(classObject, parameters);
                                        testResult.State = ExecutionState.Passed;
                                    }
                                    stopwatch.Stop();

                                    if (testResult.State != ExecutionState.Skipped)
                                    {
                                        testResult.ExecutionTime = stopwatch.Elapsed;
                                        testResult.End = DateTime.Now;
                                    }

                                    NextUnitTestExecutionContext.TestResults.Add(testResult);
                                    OnAfterTestRun(new ExecutionEventArgs(method, testResult));
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
