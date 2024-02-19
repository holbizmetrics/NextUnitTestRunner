#define COMBINATOR_TEST

using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Asserts;
using System.ComponentModel;

namespace NextUnit.TestRunner.TestRunners
{
    /// <summary>
    /// TestRunner4: This will be the first sophisticated TestRunner.
    /// 
    /// Additionally added compared to the last one:
    /// 
    /// The loops have finally been either resolved or deleted.
    /// This will be the first and only TestRunner so far with the following mechanism involved:
    /// 
    /// InstanceCreationBehavior: 
    /// 
    /// We can decide HOW and IF we want the objects derived from the types
    /// to be recreated or read out of the case or even a completely custom behavior can be implemented.
    /// 
    /// This will automatically allow us as well to use the constructor
    /// to initialize a test. And IDispose to clean it up.
    /// 
    /// Thus, if we use any InstanceCreationBehavior that recreates instance
    /// objects for every test a Guid for example would be always
    /// a different object, thus only once defined in a test.
    /// 
    /// If we used a cached one every time the Guid would be the same
    /// because for sure it's not being recreated.
    /// 
    /// AttributeLogicMapper:
    /// 
    /// The attribute logic mapper can ALSO be exchanged.
    /// Which opens total freedom in combination with the other customizable
    /// entities.
    /// 
    /// Threading:
    /// 
    /// We can also decide if we want each test to be run in its own thread
    /// or just everything sequentially.
    /// 
    /// It's not like other TestRunners where for example in one framework
    /// everything is parallel and we can't easily say exclude from being run in a thread.
    /// Or others where everyhing is sequentially and we can mark it with a thread
    /// attribute to be run in a thread.
    /// 
    /// The user can be leveraging the eventhandler to implement own logic for documentation purposes, etc.
    /// 
    /// Before Test Suite Running the user may choose if all methods will be executed in different threads.
    /// If not, this will happen sequentially.
    /// 
    /// </summary>
    public class TestRunner4 : TestRunner, ITestRunner4, IDisposable
    {
        public event ExecutionEventHandler BeforeTestRun;
        public event ExecutionEventHandler AfterTestRun;
        public event ExecutionEventHandler TestExecuting;

        public event ExecutionEventHandler TestRunStarted;
        public event ExecutionEventHandler TestRunFinished;
        public event ExecutionEventHandler ErrorEventHandler;

        public AttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();

        public bool UseConstructorDisposeOfTestClass { get; set; } = false;

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
        public Combinator UsedCombinator { get; set; } = new DefaultCombinator();
        public bool UseThreading { get; set; } = true;

        private bool disposedValue;
        public IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> TestMethodsPerClass { get; private set; }
        public Combinator Combinator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseCombinator { get => false; set => value = false; } //this flag can't be used here anymore because we need to use a combinator now anyway. Relict of refactoring for now.

        public IInstanceCreationBehavior InstanceCreationBehavior { get; set; } = new RecreateObjectInstanceForEachTest();

        public TestRunner4()
        {
            Guid guid = new Guid();
        }

        public TestRunner4(bool useThreading = true, bool useCombinator = false)
        {
            UseThreading = useThreading;
            UsedCombinator = useCombinator ? new DefaultCombinator() : new AdvancedCombinator();
        }

        public TestRunner4(ITestDiscoverer testDiscoverer = null, AttributeLogicMapper attributeLogicMapper = null, bool? useThreading = true, Combinator usedCombinator = null)
        {
            if (testDiscoverer != null) With(testDiscoverer);
            if (attributeLogicMapper != null) With(attributeLogicMapper);
            if (useThreading != null) WithUseThreading(useThreading.HasValue ? useThreading.Value : UseThreading);
            if (usedCombinator != null) WithUseCombinator(usedCombinator);
        }

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
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            AssemblyLoadContext.Default.Unloading += Default_Unloading;
            AssemblyLoadContext.Default.ResolvingUnmanagedDll += Default_ResolvingUnmanagedDll;
            if (!File.Exists(name))
            {
                return;
            }
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(name);

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
            TestMethodsPerClass = TestDiscoverer.Discover(types);

            foreach (var testDefinition in TestMethodsPerClass)
            {
                Type type = testDefinition.Type;

                if (InstanceCreationBehavior.OnlyInitializeAtStartBehavior)
                {
                    object instance = InstanceCreationBehavior.CreateInstance(type);
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

        public TestResult ExecuteTest((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition)
        {
            TestResult testResult = new TestResult();
            testResult.State = ExecutionState.Running;

            Type type = testDefinition.type;

            object instanceObject = InstanceCreationBehavior.CreateInstance(type);
            UsedCombinator.ProcessCombinedAttributes(testDefinition, instanceObject);
            testResult.State = ExecutionState.Passed;
            return testResult;
        }

        /// <summary>
        /// This is just a relict in here for the interface definition and shouldn't be used.
        /// It will throw an exception here.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="classInstance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Obsolete]
        public TestResult ExecuteTest(MethodInfo methodInfo, object classInstance)
        {
            throw new NotImplementedException();
        }

        #region Fluent Syntax
        public TestRunner4 With(ITestDiscoverer testDiscoverer)
        {
            TestDiscoverer = testDiscoverer;
            return this;
        }

        public TestRunner4 With(IInstanceCreationBehavior instanceCreationBehavior)
        {
            InstanceCreationBehavior = instanceCreationBehavior;
            return this;
        }

        public TestRunner4 WithUseCombinator(Combinator usedCombinator)
        {
            UsedCombinator = usedCombinator;
            return this;
        }

        public TestRunner4 With(AttributeLogicMapper attributeLogicMapper)
        {
            AttributeLogicMapper = attributeLogicMapper;
            return this;
        }

        public TestRunner4 WithUseThreading(bool useThreading)
        {
            UseThreading = useThreading;
            return this;
        }
        #endregion Fluent Syntax

        private void StartRecording()
        {

        }

        private void StopRecording()
        {

        }

        /// <summary>
        /// Executes the tests found by the TestDiscoverer.
        /// </summary>
        /// <param name="classTestMethodsAssociation"></param>
        protected async void ExecuteTests()
        {
            string machineName = NextUnitTestEnvironmentContext.MachineName;

            foreach (var definition in TestMethodsPerClass)
            {
                MethodInfo method = definition.Method;
                Type type = definition.Type;
                IEnumerable<Attribute> attributes = definition.Attributes;

                Exception lastException = null;
                TestResult testResult = new TestResult();
                try
                {
                    OnBeforeTestRun(new ExecutionEventArgs(method));

                    testResult.Namespace = method.DeclaringType.ToString();
                    testResult.Class = method.DeclaringType.Name;
                    testResult.Workstation = machineName;
                    testResult.DisplayName = method.Name;

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    OnTestExecuting(new ExecutionEventArgs(method));

                    testResult = ExecuteTest(definition);

                    testResult.Start = DateTime.Now;

                    stopwatch.Stop();

                    if (testResult.State != ExecutionState.Skipped)
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
                        testResult.State = ExecutionState.Failed;
                        OnError(new ExecutionEventArgs(method, testResult, lastException));
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

    public interface ITestRunner4 : ITestRunner3
    {
        Combinator Combinator { get; set; }
        IInstanceCreationBehavior InstanceCreationBehavior { get; set; }
        TestResult ExecuteTest((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition);
    }
}