using System.Diagnostics;
using System.Reflection;
using NextUnitTestRunner.Assertions;
using NextUnitTestRunner.Extensions;
using NextUnitTestRunner.TestAttributes;

namespace NextUnitTestRunner
{
    public class TestRunner
    {
        Dictionary<int, MethodInfo> classTypeMethodInfosAssociation = new Dictionary<int, MethodInfo>();
        public void Run(Type type = null)
        {
            Type[] types = type == null ? Assembly.GetExecutingAssembly().GetTypes() : type.Assembly.GetTypes();
            Type[] classes = types.Where(t => t.IsClass).ToArray();

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

                            try
                            {
                                for (int i = 0; i < executionCount; i++)
                                {
                                    Trace.WriteLine($"Running: {method.ReflectedType} -> {method}");
                                    //classTypeMethodInfosAssociation.Add(method.GetHashCode(), method);
                                    Stopwatch stopwatch = Stopwatch.StartNew();
                                    method.Invoke(classObject, parameters);
                                    Trace.WriteLine($"Time to execute: {stopwatch.ElapsedMilliseconds}");
                                    stopwatch.Stop();
                                    Trace.WriteLine("");
                                }
                            }
                            catch (AssertException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (TargetInvocationException ex)
                            {
                                if (ex.InnerException != null) Console.WriteLine(ex.InnerException);
                                else
                                {
                                    Trace.WriteLine(ex);
                                }
                            }
                            catch (TargetParameterCountException ex)
                            {
                                Trace.WriteLine(ex);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine(ex);
                            }
                        }
                    }
                }

            }
        }

        public Dictionary<int, MethodInfo> ExecutedMethodsPerClass
        {
            get { return classTypeMethodInfosAssociation; }
        }
    }
}
