using System.Diagnostics;
using System.Reflection;
using NextUnit.Core.TestAttributes;
using NextUnitTestRunner.Assertions;
using NextUnitTestRunner.Extensions;

namespace NextUnitTestRunner
{
    public interface ITestRunner
    {
        void Run(Type type);
    }

    /// <summary>
    /// This is just a first TestRunner as a proof of concept.
    /// </summary>
    public class TestRunner : ITestRunner
    {
        protected Dictionary<int, MethodInfo> classTypeMethodInfosAssociation = new Dictionary<int, MethodInfo>();
        protected TestDiscoverer discoverer = new TestDiscoverer();

        public virtual void Run(Type type = null)
        {
            Type[] types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            Type[] classes = types.Where(t => t.IsClass).ToArray();

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
                                    //classTypeMethodInfosAssociation.Add(method.GetHashCode(), method);

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

        public Dictionary<int, MethodInfo> ExecutedMethodsPerClass
        {
            get { return classTypeMethodInfosAssociation; }
        }
    }
}
