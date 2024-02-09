#define COMBINATOR_TEST

using System.Diagnostics;
using System.Reflection;
using NextUnit.Core.TestAttributes;
using System.Runtime.Loader;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.Asserts;

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
    public class TestRunner3 : TestRunner, ITestRunner3, IDisposable
    {
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;

        public AttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();

        /// <summary>
        /// If set for each test run the class object will be reinstantiated. Not implemented, yet.
        /// </summary>
        public bool RecreateClassObject { get; } = false;

        /// <summary>
        /// Without combinator:
        /// 
        /// The attributes will be just used AS IS.
        /// 
        /// This means in several cases it may not make sense to run the test several times.
        /// e.g.  
        ///
        /// </summary>
        public bool UseCombinator { get; set; } = false;
        public bool UseThreading { get; set; } = true;

        public Dictionary<Type, object> InstanceObjects = new Dictionary<Type, object>();
        private bool disposedValue;

        public IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> TestMethodsPerClass { get; private set; }

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
        /// Will be triggered if a test is executing.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestExecuting(ExecutionEventArgs e)
        {
            TestExecuting?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if a test run is started.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestRunStarted(ExecutionEventArgs e)
        {
            TestRunStarted?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if a test run is ending.
        /// </summary>
        /// <param name="e"></param>
        protected void OnTestRunFinished(ExecutionEventArgs e)
        {
            TestRunFinished?.Invoke(this, e);
        }

        /// <summary>
        /// Will be triggered if an error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected void OnError(ExecutionEventArgs e)
        {
            ErrorEventHandler?.Invoke(this, e);
        }

        public override void Run(object objectToGetTypeFrom)
        {
            Run(objectToGetTypeFrom.GetType());
        }

        /// <summary>
        /// Runs the test.
        /// If there is an error occurring an error event will be triggered.
        /// </summary>
        /// <param name="name"></param>
        public override void Run(string name, params Type[] types)
        {
            TestRunnerAssemblyLoadContext.Default.Resolving += Default_Resolving;
            TestRunnerAssemblyLoadContext.Default.Unloading += Default_Unloading;
            TestRunnerAssemblyLoadContext.Default.ResolvingUnmanagedDll += Default_ResolvingUnmanagedDll;
            if (!File.Exists(name))
            {
                return;
            }
            var assembly = TestRunnerAssemblyLoadContext.Default.LoadFromAssemblyPath(name);

            if (types == null || types.Length == 0)
            {
                types = assembly.GetTypes();
            }
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
            if (obj.IsCollectible)
            {
                obj.Unload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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

            //This will already discover all the tests for all types.
            //So this could, respectively SHOULD also be used by the TestDiscoverer now.
            TestMethodsPerClass = ReflectionExtensions.GetMethodsWithAttributesAsIEnumerableGeneric2<Attribute>(types);

            //thus, we only need to create the instance objects per type here.
            foreach (var testDefinition in TestMethodsPerClass)
            {
                (Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition = ((Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes))testDefinition;
                Type definitionType = definition.type;
                if (!InstanceObjects.ContainsKey(definition.type))
                {
                    InstanceObjects.Add(definitionType, Activator.CreateInstance(definitionType));
                }
            }

            //before:   the attributes and class instances were done in a loop.
            //          this definitely is costing some performance.
            //
            //now:      - lets detect the attributes first.
            //          - let's create class instances for each object where tests were found.
            //          - and only THEN: execute the tests.

            if (UseThreading)
            {
                Thread thread = new Thread(() =>
                {
                    ExecuteTests();
                });
                thread.Start();
                //thread.Join();
            }
            else
            {
                ExecuteTests();
            }

            OnTestRunFinished(new ExecutionEventArgs());
        }

        public TestResult ExecuteTest(MethodInfo method, object classObject = null)
        {
            object[] parameters = null;
            IEnumerable<Attribute> attributes = method.GetCustomAttributes();
            TestResult testResult = null;
            if (attributes.Any(x => x.GetType() == typeof(TestAttribute) || x.GetType().BaseType == typeof(TestAttribute)))
            {
                int executionCount = 1;
                foreach (Attribute attribute in attributes)
                {
                    Exception lastException = null;
                    try
                    {
                        OnBeforeTestRun(new ExecutionEventArgs(method));
                        testResult = new TestResult();
                        testResult.Namespace = method.DeclaringType.ToString();
                        testResult.Class = method.DeclaringType.Name;
                        testResult.Workstation = Environment.MachineName;
                        testResult.DisplayName = method.Name;

                        Stopwatch stopwatch = Stopwatch.StartNew();
                        OnTestExecuting(new ExecutionEventArgs(method));
                        //Since we exclude the test marker attribute, a test logic handler should be found for each of the implemented framework attribute.

                        testResult.Start = DateTime.Now;
                        //if being used attributes will be handled in a new way.
                        //which will be become the proper way.
                        //but until then it won't be 
                        if (UseCombinator)
                        {
                            var customAttributes = method.GetCustomAttributes<Attribute>().ToArray();
                            var combinator = new AttributeCombinator(customAttributes);
                            combinator.ProcessCombinedAttributes(method, classObject);
                        }
                        else
                        {
                            var handler = AttributeLogicMapper.GetHandlerFor(attribute);

                            handler?.ProcessAttribute(attribute, method, classObject);
                        }

                        testResult.State = ExecutedState.Passed;
                        stopwatch.Stop();

                        if (testResult.State != ExecutedState.Skipped)
                        {
                            testResult.ExecutionTime = stopwatch.Elapsed;
                            testResult.End = DateTime.Now;
                        }
                    }
                    //catch (AssertException ex)
                    //{
                    //    lastException = ex;
                    //    Trace.WriteLine(ex.Message);
                    //}
                    //catch (TargetInvocationException ex)
                    //{
                    //    lastException = ex;
                    //    if (ex.InnerException != null)
                    //    {
                    //        Trace.WriteLine(ex.InnerException);
                    //    }
                    //    else
                    //    {
                    //        Trace.WriteLine(ex);
                    //    }
                    //}
                    //catch (TargetParameterCountException ex)
                    //{
                    //    lastException = ex;
                    //    Trace.WriteLine(ex);
                    //}
                    //catch (Exception ex)
                    //{
                    //    lastException = ex;
                    //    Trace.WriteLine(ex);
                    //}
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
                            testResult.State = ExecutedState.Failed;
                            OnError(new ExecutionEventArgs(method, testResult, lastException));
                        }
                    }
                }
            }
            return testResult;
        }

        public TestRunner3()
        {

        }

        public TestRunner3(bool useThreading = true, bool useCombinator = false)
        {
            this.UseThreading = useThreading;
            this.UseCombinator = useCombinator;
        }

        public TestRunner3(ITestDiscoverer testDiscoverer = null, AttributeLogicMapper attributeLogicMapper = null, bool? useThreading = true, bool? useCombinator = false)
        {
            if (testDiscoverer != null) With(testDiscoverer);
            if (attributeLogicMapper != null) With(attributeLogicMapper);
            if (useThreading != null) WithUseThreading(useThreading.HasValue ? useThreading.Value : UseThreading);
            if (useCombinator != null) WithUseCombinator(useCombinator.HasValue ? useCombinator.Value : UseCombinator);
        }

        #region Fluent Syntax
        public TestRunner3 With(ITestDiscoverer testDiscoverer)
        {
            this.TestDiscoverer = testDiscoverer;
            return this;
        }

        public TestRunner3 WithUseCombinator(bool useCombinator)
        {
            this.UseCombinator = useCombinator;
            return this;
        }

        public TestRunner3 With(AttributeLogicMapper attributeLogicMapper)
        {
            this.AttributeLogicMapper = attributeLogicMapper;
            return this;
        }

        public TestRunner3 WithUseThreading(bool useThreading)
        {
            this.UseThreading = useThreading;
            return this;
        }
        #endregion Fluent Syntax

        /// <summary>
        /// Executes the tests found by the TestDiscoverer.
        /// </summary>
        /// <param name="classTestMethodsAssociation"></param>
        protected async void ExecuteTests()
        {
            string machineName = NextUnitTestEnvironmentContext.MachineName;
            foreach (var testDefinition in TestMethodsPerClass)
            {
                (Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition = testDefinition;
                Type definitionType = definition.type;

                MethodInfo method = definition.methodInfo;
                object classObject = RecreateClassObject ? (InstanceObjects[definitionType] = Activator.CreateInstance(definitionType)) : InstanceObjects[definitionType];

                //TODO:
                //This will also have to be done in a totally different way.
                //If handled correctly the type check and this initialization here shouldn't be needed anymore.
                Type[] unallowedTypes = new Type[] { typeof(TestAttribute), typeof(GroupAttribute), typeof(SkipAttribute) };

                foreach (Attribute attribute in definition.Attributes)
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

                        //TODO:
                        //if being used attributes will be handled in a new way.
                        //which will be become the proper way.
                        //but until then it won't be 
                        if (UseCombinator)
                        {
                            var customAttributes = method.GetCustomAttributes<Attribute>().ToArray();
                            var combinator = new AttributeCombinator(customAttributes);
                            combinator.ProcessCombinedAttributes(method, classObject);
                        }
                        else
                        {
                            //TODO:
                            //avoid false positives.
                            //this has definitely to be improved by the combinator.

                            //this if shouldn't be needed there as soon as the detection using 
                            //TestMethodsPerClass = ReflectionExtensions.GetMethodsWithAttributesAsIEnumerableGeneric2<Attribute>(types);
                            //works correctly.

                            //Definitely the check for async would - for the current design - have to move into the single Attribute Logic Mappers.
                            if (!unallowedTypes.Contains(attribute.GetType()) && attribute.GetType().Namespace.Contains("NextUnit."))
                            {
                                var handler = AttributeLogicMapper.GetHandlerFor(attribute);
                                handler?.ProcessAttribute(attribute, method, classObject);
                                if (handler != null)
                                {
                                    testResult.State = ExecutedState.Passed;
                                }
                            }
                            else if ((definition.Attributes.Count() == 1 && attribute is TestAttribute) || method.HasAsyncMethodAttributes())
                            {
                                if (method.IsAsyncMethod())
                                {
                                    var task = (Task)method.Invoke(classObject, null); // Assuming no parameters for simplicity
                                    await task.ConfigureAwait(false);
                                    // Handle the result of the async test execution
                                }
                                else
                                {
                                    method.Invoke(classObject, null);
                                    testResult.State = ExecutedState.Passed;
                                }
                            }
                        }

                        testResult.Start = DateTime.Now;

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
                            testResult.State = ExecutedState.Failed;
                            OnError(new ExecutionEventArgs(method, testResult, lastException));
                        }
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TestRunner3()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}