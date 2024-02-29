#define COMBINATOR_TEST

using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Asserts;
using NextUnit.Core.Combinators;
using NextUnit.Core;
using NextUnit.Core.Extensions;
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.HardwareContext.Extensions;
using NextUnit.TestRunner.TestRunners.TestRunner5;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using TestResult = NextUnit.Core.TestResult;

namespace NextUnit.TestRunner.TestRunners.NewFolder
{
    /// <summary>
    /// TestRunner5: This will be the first sophisticated TestRunner.
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
    public partial class TestRunner5 : TestRunner4, ITestRunner5, IDisposable
    {
        public Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> TestMethodDelegates { get; set; } = null;

        public IAttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();

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
        public Combinator UsedCombinator { get; set; } = new DefaultCombinator { AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper() };
        public bool UseThreading { get; set; } = true;

        private bool disposedValue;

        public TestRunner5()
        {
        }

        public TestRunner5(bool useThreading = true, bool useCombinator = false)
        {
            UseThreading = useThreading;
            UsedCombinator = useCombinator ? new DefaultCombinator() : new AdvancedCombinator();
        }

        public TestRunner5(ITestDiscoverer testDiscoverer = null, AttributeLogicMapper attributeLogicMapper = null, bool? useThreading = true, Combinator usedCombinator = null)
        {
            if (testDiscoverer != null) With(testDiscoverer);
            if (attributeLogicMapper != null) With(attributeLogicMapper);
            if (useThreading != null) WithUseThreading(useThreading.HasValue ? useThreading.Value : UseThreading);
            if (usedCombinator != null) WithUseCombinator(usedCombinator);
        }

        public bool PreferDelegate
        {
            get { return ReflectionExtensions.PreferDelegate; }
            set { ReflectionExtensions.PreferDelegate = value; }
        }

        /// <summary>
        /// Runs the tests for this specicif object type.
        /// </summary>
        /// <param name="objectToGetTypeFrom"></param>
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
            Assembly loadedAssembly = null;

            loadedAssembly = AssemblyLoadContext.Default.Assemblies.Where(x => x.GetName().Name == Path.GetFileNameWithoutExtension(name)).FirstOrDefault();

            if (loadedAssembly == null)
            {
                loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(name);
            }
            if (types == null || types.Length == 0)
            {
                try
                {
                    types = loadedAssembly.GetTypes();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("LoadFromAssembly GetTypes() caused an error:");
                    Trace.WriteLine("--------------------------------------------");
                    Trace.WriteLine(ex.GetAllMessages());
                }
            }
            if (types == null)
            {
                Trace.WriteLine("No executable potential types found.");
                return;
            }
            Run(types);
            Default_Unloading(AssemblyLoadContext.Default);
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
            TestRunnerAssemblyLoadContext.UnloadAssemblyContext(AssemblyLoadContext.Default);
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
            return LoadAdditionalAssemblies(arg1, arg2);
        }

        /// <summary>
        /// Check for additional assemblies to load.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        protected Assembly? LoadAdditionalAssemblies(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            // Define the custom path where your assemblies are located
            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyAssemblies");

            // Construct the path to the assembly file
            string assemblyFilePath = Path.Combine(assemblyPath, $"{arg2.Name}.dll");

            // Check if the assembly file exists
            if (File.Exists(assemblyFilePath))
            {
                // Load and return the assembly from the file
                return arg1.LoadFromAssemblyPath(assemblyFilePath);
            }

            // If the assembly was not found in the custom path, return null to let the default resolver handle it
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void Run(params Type[] types)
        {
            //Will be fired when the complete test run has started (for the moment also valid for: per assembly).
            OnTestRunStarted(new ExecutionEventArgs());

            NextUnitTestExecutionContext.TestRunStart = DateTime.Now;
            DiscoverTests(types);
            CreateInstantiatedObjects();
            TestMethodDelegates = CreateTestDelegates(TestMethodsPerClass);

            // ok this works. At least tried for some methods already.
            // They can be executed like this then:
            // testMethodDelegates.First().Value.@delegate.DynamicInvoke();
            // new method to execute tests by creating delegates.

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

            //Will be fired when the complete test run has finished (for the moment also valid for: per assembly).
            OnTestRunFinished(new ExecutionEventArgs());
        }

        private void DiscoverTests(Type[] types)
        {
            //This will already discover all the tests for all types.
            //So this could, respectively SHOULD also be used by the TestDiscoverer now.
            TestMethodsPerClass = TestDiscoverer.Discover(types);

        }
        /// <summary>
        /// Discovers creates a delegate list from discovered tests
        /// This may be part of the testdiscoverer or still stay in the testrunner. Do not know, yet.
        /// </summary>
        public Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> CreateTestDelegates(IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> testMethodsPerClass)
        {
            Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> testMethodDelegates = new Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)>();
            foreach (var testDefinition in testMethodsPerClass)
            {
                testMethodDelegates.Add(
                    $"{testDefinition.Method.DeclaringType.FullName}.{testDefinition.Method.Name}",
                    (testDefinition.Type, testDefinition.Method, testDefinition.Attributes,
                        testDefinition.Method.CreateTestDelegate(InstanceCreationBehavior.CreateInstance(testDefinition.Type))));
            }
            return testMethodDelegates;
        }

        private void CreateInstantiatedObjects()
        {
            foreach (var testDefinition in TestMethodsPerClass)
            {
                Type type = testDefinition.Type;

                if (InstanceCreationBehavior.OnlyInitializeAtStartBehavior)
                {
                    object instance = InstanceCreationBehavior.CreateInstance(type);
                }
            }
        }

        /// <summary>
        /// Executes a test.
        /// 
        /// Will be called by
        /// 
        /// 1. By TestAdapter.ExecuteTest
        /// 2. By the TestRunner.ExecuteTest itself.
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public TestResult ExecuteTest((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition)
        {
            Type type = testDefinition.type;
            object instanceObject = InstanceCreationBehavior.CreateInstance(type); // this will for sure take a little longer if the test instance object needs to be reinstantiated for every TestMethod run.
            Task<TestResult> task = UsedCombinator.ProcessCombinedAttributes(testDefinition, instanceObject);
            NextUnitTestExecutionContext.TestResults.Add(task.Result);
            return task.Result;
        }

        //private ITestLifecycle GetLifecycleHooks()
        //{

        //}

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
        public TestRunner5 With(ITestDiscoverer testDiscoverer)
        {
            TestDiscoverer = testDiscoverer;
            return this;
        }

        public TestRunner5 With(IInstanceCreationBehavior instanceCreationBehavior)
        {
            InstanceCreationBehavior = instanceCreationBehavior;
            return this;
        }

        public TestRunner5 WithUseCombinator(Combinator usedCombinator)
        {
            UsedCombinator = usedCombinator;
            return this;
        }

        public TestRunner5 With(AttributeLogicMapper attributeLogicMapper)
        {
            AttributeLogicMapper = attributeLogicMapper;
            return this;
        }

        public TestRunner5 WithUseThreading(bool useThreading)
        {
            UseThreading = useThreading;
            return this;
        }
        #endregion Fluent Syntax

      

        public TestResult Execute((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) test)
        {
            object instanceObject = InstanceCreationBehavior.CreateInstance(test.type); // this will for sure take a little longer if the test instance object needs to be reinstantiated for every TestMethod run.
            Task<TestResult> task = UsedCombinator.ProcessCombinedAttributes(test, instanceObject);
            return task.Result;
        }

        protected Stopwatch Stopwatch { get; private set; } = null;

        /// <summary>
        /// Executes the tests found by the TestDiscoverer.
        /// </summary>
        /// <param name="classTestMethodsAssociation"></param>
        protected async void ExecuteTests()
        {
            NextUnitTestExecutionContext.TestResults.Clear();
            NextUnitTestExecutionContext.TestRunStart = DateTime.Now;

            TestExecutor testExecutor = new TestExecutor()
                .With(this.InstanceCreationBehavior)
                .With(this.UsedCombinator);

            // Setup default execution pipeline.
            // What could be done as well to optimize is:
            // Only if an attribute in all tests is found, for Before/After then initialize the pipeline with Before/After. Otherwise initialize only execute.

            // This structure below even later on would allow us with ease - if it makes sense - to create a multicastdelegate (and caching all of them) with Before/After only where needed.
            //testExecutor.AddToPipeline(new TestDelegate(BeforeTestRunExecution));
            testExecutor.AddToPipeline(new TestDelegate(ExecuteTest));
            //testExecutor.AddToPipeline(new TestDelegate(AfterTestRunExecution));

            foreach (var definition in TestMethodDelegates)
            {
                MethodInfo method = definition.Value.methodInfo;
                Type type = definition.Value.type;
                IEnumerable<Attribute> attributes = definition.Value.attributes;

                Exception lastException = null;
                TestResult testResult = TestResult.Empty;

                // start the recording of the testResult
                PrepareTestResult(definition.Value, testResult, type);
                try
                {
                    OnBeforeTestRun(new ExecutionEventArgs(method));
                    OnTestExecuting(new ExecutionEventArgs(method));
                    Stopwatch = Stopwatch.StartNew();
                    testExecutor.Execute(definition.Value); // Assuming RunTest handles individual test execution
                    Pass(testResult);
                }
                catch (AggregateException ex)
                {
                    lastException = ex;
                    Trace.WriteLine(ex);
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
                    //Ends the recording of the testResult
                    EndTestResult(testResult);

                    // After test actions
                    // Clean up, log results, etc.
                    OnAfterTestRun(new ExecutionEventArgs(method, testResult));
                    if (lastException != null)
                    {
                        testResult.StackTrace = lastException?.StackTrace;
                        Fail(testResult);
                        OnError(new ExecutionEventArgs(method, testResult, lastException));
                    }

                    // Unload assemblies if necessary
                    Default_Unloading(TestRunnerAssemblyLoadContext.Default);

                    //Trace.Assert(lastException == null);
                }
            }
            NextUnitTestExecutionContext.TestRunEnd = DateTime.Now;

            // Log summary, calculate total run time, etc.
        }

        private void Fail(TestResult testResult)
        {
            testResult.State = ExecutionState.Failed;
        }

        private void Pass(TestResult testResult)
        {
            testResult.State = ExecutionState.Passed;
        }

        private void Skip(TestResult testResult)
        {
            testResult.State = ExecutionState.Skipped;
        }

        private void PrepareTestResult((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition, TestResult testResult, Type declaringType)
        {
            testResult.Namespace = declaringType.ToString();
            testResult.Class = declaringType.Name;
            testResult.Workstation = Environment.MachineName;
            testResult.DisplayName = testDefinition.methodInfo.Name;
            testResult.Start = DateTime.Now;
            testResult.State = ExecutionState.Running;
            Stopwatch = Stopwatch.StartNew();
        }

        private void EndTestResult(TestResult testResult)
        {
            Stopwatch.Stop();
            testResult.ExecutionTime = Stopwatch.Elapsed;
            testResult.End = DateTime.Now;
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

    public interface ITestRunner5 : ITestRunner4
    {
        public bool PreferDelegate { get; set; }
    }

}