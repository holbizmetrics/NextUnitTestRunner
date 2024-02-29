using Microsoft.CodeAnalysis;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NextUnit.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public delegate void TestMethodDelegate();
        public delegate void TestMethodDelegateWithParams(params object[] parameters);
        public delegate Task AsyncTestMethodDelegate();
        public delegate Task AsyncTestMethodDelegateWithParams(params object[] parameters);

        private static DelegateTypeFactory DelegateTypeFactoryCache = null;


        public static bool PreferDelegate { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="instance"></param>
        /// <param name="delegate"></param>
        public static object Invoke(this MethodInfo methodInfo, object instance, Delegate @delegate = null, object[] parameters = null)
        {
            if (@delegate == null || !PreferDelegate)
            {
                return methodInfo.Invoke(instance, parameters);
            }
            else
            {
                return @delegate.DynamicInvoke(parameters);
            }
        }

        public static Delegate CreateTestDelegate(this MethodInfo method, object instance = null)
        {
            if (DelegateTypeFactoryCache == null)
            {
                DelegateTypeFactoryCache = new DelegateTypeFactory();
            }

            Type delegateType = DelegateTypeFactoryCache.CreateDelegateType(method);

            // Check if the method is static
            if (method.IsStatic)
            {
                // For static methods, no target object is required
                return Delegate.CreateDelegate(delegateType, null, method);
            }
            else
            {
                // Ensure instance is not null for instance methods
                if (instance == null)
                {
                    throw new ArgumentNullException(nameof(instance), "An instance is required for instance methods.");
                }
                return Delegate.CreateDelegate(delegateType, instance, method);
            }
        }


        /// <summary>
        /// Creates a delegate from the method.
        /// Should also work for async methods.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Delegate CreateTestDelegate(this MethodInfo method)
        {
            bool isAsync = typeof(Task).IsAssignableFrom(method.ReturnType);
            bool hasParameters = method.GetParameters().Any();

            if (isAsync)
            {
                if (hasParameters)
                {
                    return Delegate.CreateDelegate(typeof(AsyncTestMethodDelegateWithParams), null, method);
                }
                else
                {
                    return Delegate.CreateDelegate(typeof(AsyncTestMethodDelegate), null, method);
                }
            }
            else
            {
                if (hasParameters)
                {
                    return Delegate.CreateDelegate(typeof(TestMethodDelegateWithParams), null, method);
                }
                else
                {
                    return Delegate.CreateDelegate(typeof(TestMethodDelegate), null, method);
                }
            }
        }

        public static bool IsAsyncMethod(this MethodInfo methodInfo)
        {
            return typeof(Task).IsAssignableFrom(methodInfo.ReturnType);
        }

        public static bool HasAsyncMethodAttributes(this MethodInfo methodInfo)
        {
            return HasSpecificCustomAttributes(methodInfo, typeof(NullableContextAttribute), typeof(AsyncStateMachineAttribute), typeof(DebuggerStepThroughAttribute));
        }

        public static bool HasSpecificCustomAttributes(this MethodInfo methodInfo, params Type[] attributeTypes)
        {
            var customAttributes = methodInfo.GetCustomAttributes().ToList();
            return attributeTypes.Any(attributeType => customAttributes.Any(attribute => attributeType.IsInstanceOfType(attribute)));
        }


        public static MethodInfo[] GetMethods(this Type type, string[] methodNames, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
        {
            List<MethodInfo> list = new List<MethodInfo>();
            foreach (string methodName in methodNames)
            {
                MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
                if (methodInfo != null)
                {
                    list.Add(methodInfo);
                }
            }
            return list.ToArray();
        }

        public static void AssertMethodParameters<T>(
             this MethodInfo methodInfo,
             T testInstance,
             Action<object, ParameterInfo> assertAction)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (assertAction == null) throw new ArgumentNullException(nameof(assertAction));

            // Invoke the method with default parameters to simulate AutoData-like behavior
            var parameters = methodInfo.GetParameters();
            var parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                parameterValues[i] = GenerateDefaultValue(parameter.ParameterType);
            }

            // Execute the method to populate parameters with "auto-generated" data
            methodInfo.Invoke(testInstance, parameterValues);

            // Assert each parameter based on the provided assertAction
            foreach (var parameter in parameters)
            {
                var parameterValue = parameterValues[parameter.Position];
                assertAction(parameterValue, parameter);
            }
        }

        private static object GenerateDefaultValue(Type type)
        {
            // Simulate AutoData behavior by generating default values
            // This is a simplified example, you might need a more sophisticated solution for generating test data
            if (type == typeof(int)) return default(int);
            if (type == typeof(int?)) return default(int?);
            if (type == typeof(bool)) return default(bool);
            if (type == typeof(bool?)) return default(bool?);
            if (type == typeof(string[])) return new string[0];
            if (type == typeof(List<string>)) return new List<string>();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Activator.CreateInstance(type);
            }
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetInterface<T>(this Type type)
        {
            return type.GetInterfaces().FirstOrDefault(x => x == typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Type type, string name)
        {
            return GetValue<T>(type, name, type);
        }

        /// <summary>
        /// Gets the values of an object as a dictionary which is: property name, value object pair.
        /// </summary>
        /// <param name="objectToGetValuesFrom"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetValues(this object objectToGetValuesFrom)
        {
            Type objectType = objectToGetValuesFrom.GetType();
            string[] invalidProperties = new string[] { "DeclaringMethod", "GenericParameterAttributes", "GenericParameterPosition" };
            PropertyInfo[] propertyInfos = objectType.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (var propertyInfo in propertyInfos)
            {
                string valueName = propertyInfo.Name;
                if (!propertyInfo.CanRead) continue;
                if (invalidProperties.Contains(valueName))
                {
                    continue;
                }
                values.Add(valueName, propertyInfo.GetValue(objectToGetValuesFrom, null));
            }
            return values;
        }

        /// <summary>
        /// Gets generally a value from an object no matter if it's a field/property/method, no matter if it's static or not, public or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Type type, string name, object instance)
        {
            MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            object returnValue = null;
            if (methodInfo != null)
            {
                returnValue = methodInfo.Invoke(instance, null);
                return (T)returnValue;
            }

            PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                returnValue = propertyInfo.GetValue(instance, null);
                return (T)returnValue;
            }

            FieldInfo field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                returnValue = field.GetValue(instance);
                return (T)returnValue;
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static List<object> GetValues(Type t, object instance)
        {
            return null;
        }

        /// <summary>
        /// Entity here is either a public field, a property, or a method that returns a value.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="instance"></param>
        /// <param name="entityNames"></param>
        /// <returns></returns>
        public static List<object> GetValues(Type t, object instance, string entityNames)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsComparable(this Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type);
        }

        /// <summary>
        /// Gets ITestContext implementing custom attributes.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static List<ITestContext> Test(MethodInfo method)
        {
            var testContextAttributes = method.GetCustomAttributes()
                .Where(attr => attr is ITestContext)
                .Cast<ITestContext>()
                .ToList();
            return testContextAttributes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="message"></param>
        /// <param name="depth"></param>
        /// <exception cref="AssertException"></exception>
        public static void CompareProperties(object a, object b, string message = "", int depth = 0)
        {
            // Check for reference equality first
            if (ReferenceEquals(a, b)) return;

            // If either is null and they are not the same instance, they are not equal
            if (a == null || b == null)
                throw new AssertException($"{message}: One of the objects is null.");

            // Handle arrays and collections
            if (a is IEnumerable && b is IEnumerable)
            {
                var enumeratorA = ((IEnumerable)a).GetEnumerator();
                var enumeratorB = ((IEnumerable)b).GetEnumerator();
                while (enumeratorA.MoveNext() && enumeratorB.MoveNext())
                {
                    if (!enumeratorA.Current.Equals(enumeratorB.Current))
                        throw new AssertException($"{message}: Elements in the collections do not match.");
                }
                return;
            }

            // Check if both objects are of the same type
            if (a.GetType() != b.GetType())
                throw new AssertException($"{message}: Objects are of different types.");

            // Limit the recursion depth to prevent stack overflow
            if (depth > 10)
                throw new AssertException($"{message}: Recursion depth limit exceeded.");

            // Compare each property
            PropertyInfo[] properties = a.GetType().GetProperties();
            foreach (var prop in properties)
            {
                object valueA = prop.GetValue(a);
                object valueB = prop.GetValue(b);

                if (valueA is ValueType || valueA is string)
                {
                    if (!Equals(valueA, valueB))
                        throw new AssertException($"{message}: Property {prop.Name} does not match. {valueA} != {valueB}");
                }
                else if (valueA != null && valueB != null)
                {
                    // Recursively compare complex object properties
                    CompareProperties(valueA, valueB, message, depth + 1);
                }
                else if (valueA != valueB) // One is null, the other is not
                    throw new AssertException($"{message}: Property {prop.Name} does not match. One is null and the other is not.");
            }
        }

        /// <summary>
        /// Check for a type if it is equatable (contains IEquatable)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEquatable(this Type type)
        {
            // Construct the specific type of IEquatable<T> where T is the parameter type
            Type equatableType = typeof(IEquatable<>).MakeGenericType(type);
            bool isEquatable = equatableType.IsAssignableFrom(type);
            return isEquatable;
        }

        /// <summary>
        /// Finds a nested interface in a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type FindNestedTypeContainingInterface(this Type type)
        {
            //type.GetNestedType().GetInterface();
            return typeof(void);
        }

        /// <summary>
        /// Gets all the values on attribute as a dictionary object: property name, value pair.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetAttributeValues(this Attribute attribute)
        {
            return attribute.GetValues();
        }

        /// <summary>
        /// Gets the value of a marked attribute.
        /// 
        /// If a property is marked like this:
        /// 
        /// [NextUnitValue]
        /// public string Value {get; set;} = "Hallo"
        /// 
        /// this would return "Value/"Hallo" pair in that case, etc.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetMarkedAttributeValues(this Attribute attribute)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            PropertyInfo[] propertyInfos = attribute.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                NextUnitValueAttribute nextUnitValueAttribute = propertyInfo.GetCustomAttribute<NextUnitValueAttribute>();
                if (nextUnitValueAttribute == null) continue;
                values.Add(propertyInfo.Name, propertyInfo.GetValue(attribute));
            }
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetMethods(params Type[] types)
        {
            return GetMethodsWithAttributes(types);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<(Type t, MethodInfo m, IEnumerable<T>)> GetMethods<T>(params Type[] types) where T : Attribute
        {
            return GetMethodsWithAttributesAsIEnumerableGeneric<T>(types);
        }

        public static IEnumerable<(Type t, MethodInfo m, IEnumerable<T>)> GetMethods2<T>(params Type[] types) where T : Attribute
        {
            return GetMethodsWithAttributesAsIEnumerableGeneric2<T>(types);
        }

        //public static (string fileName, int lineNumber) GetSourceInformation(this MethodInfo methodInfo)
        //{
        //    // Create an instance of the method to get the stack trace
        //    var instance = new StackTrace(methodInfo);

        //    foreach (var frame in instance.GetFrames())
        //    {
        //        var method = frame.GetMethod();
        //        if (method == methodInfo)
        //        {
        //            // Get the file name and line number
        //            var fileName = frame.GetFileName();
        //            var lineNumber = frame.GetFileLineNumber();

        //            return (fileName, lineNumber);
        //        }
        //    }

        //    return (null, 0);
        //}

        /// <summary>
        /// Gets methods marked with the CommonTestAttribute.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetMethodsWithAttributes(params Type[] types)
        {
            var methodsWithAttributes = from type in types
                                        from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                        let attributes = method.GetCustomAttributes<CommonTestAttribute>(true)
                                        where attributes.Any()
                                        select new
                                        {
                                            Type = type,
                                            Method = method,
                                            Attributes = attributes
                                        };

            return methodsWithAttributes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] GetAssemblyPaths()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                           // Filter to avoid assemblies without a physical location
                           .Where(assembly => !string.IsNullOrWhiteSpace(assembly.Location))
                           // Select the full path of the assembly
                           .Select(assembly => assembly.Location)
                           .ToArray();
        }

        /// <summary>
        /// Same function as before: Gets methods marked with Attribute of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetMethodsWithAttributesGeneric<T>(params Type[] types)
        {
            var methodsWithAttributes = from type in types
                                        from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                        let attributes = method.GetCustomAttributes(typeof(T), true).Cast<T>()
                                        where attributes.Any()
                                        select new
                                        {
                                            Type = type,
                                            Method = method,
                                            Attributes = attributes
                                        };

            return methodsWithAttributes;
        }

        /// <summary>
        /// Generic method to get tuples of
        /// 
        /// - Type
        /// - Method
        /// - Attribute List
        /// 
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<(Type Type, MethodInfo Method, IEnumerable<T> Attributes)> GetMethodsWithAttributesAsIEnumerableGeneric<T>(params Type[] types) where T : Attribute
        {
            return types
                .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                .Select(method => new
                {
                    Type = method.DeclaringType,
                    Method = method,
                    Attributes = method.GetCustomAttributes(typeof(T), true).Cast<T>()
                })
                .Where(x => x.Attributes.Any())
                .Select(x => (x.Type, x.Method, x.Attributes));
        }

        public static IEnumerable<(Type Type, MethodInfo Method, IEnumerable<T> Attributes)> GetMethodsWithAttributesAsIEnumerableGeneric2<T>(params Type[] types) where T : Attribute
        {
            return types
                .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                .Select(method => new
                {
                    Type = method.DeclaringType,
                    Method = method,
                    Attributes = method.GetCustomAttributes(typeof(T), true).Cast<T>()
                })
                .Where(x => x.Attributes.Any(attribute => attribute.GetType().Namespace.Contains("NextUnit.")))
                .Select(x => (x.Type, x.Method, x.Attributes));
        }

        public static IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> GetMethodsWithAttributesAsIEnumerable(params Type[] types)
        {
            return types
                .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                .Select(method => new
                {
                    Type = method.DeclaringType,
                    Method = method,
                    Attributes = method.GetCustomAttributes<Attribute>(true)
                })
                .Where(x => x.Attributes.Any())
                .Select(x => (x.Type, x.Method, x.Attributes));
        }

        public static IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> FindMethodByName(
            IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> methodsWithAttributes,
                string methodName)
        {
            return methodsWithAttributes
                .Where(x => x.Method.Name == methodName);
        }

        public static bool IsStaticClass(this Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        public static string FormatParameter(this ParameterInfo parameter)
        {
            Type type = parameter.ParameterType;
            string typeName;

            if (type.IsGenericType)
            {
                // Handle generic types
                typeName = type.GetGenericTypeDefinition().Name;
                // Remove the generic arity (`\`1`, `\`2`, etc.)
                typeName = typeName.Substring(0, typeName.IndexOf('`'));
                // Format the generic type arguments
                string genericArgs = string.Join(", ", type.GetGenericArguments().Select(FormatType));
                typeName = $"{typeName}<{genericArgs}>";
            }
            else
            {
                // Non-generic types
                typeName = FormatType(type);
            }

            return $"{typeName} {parameter.Name}";
        }

        public static string FormatType(this Type type)
        {
            // Handle array types
            if (type.IsArray)
            {
                return $"{FormatType(type.GetElementType())}[]";
            }

            // Add more type handling if necessary (e.g., nullable types)

            // Use the type's name or a predefined alias (for common types)
            switch (type.Name)
            {
                case "String": return "string";
                case "Int32": return "int";
                case "Boolean": return "bool";
                // Add other type aliases as needed
                default: return type.Name;
            }
        }

        /// <summary>
        /// Given a lambda expression that calls a method, returns the method info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo(Expression<Action> expression)
        {
            return GetMethodInfo((LambdaExpression)expression);
        }

        /// <summary>
        /// Given a lambda expression that calls a method, returns the method info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            return GetMethodInfo((LambdaExpression)expression);
        }

        /// <summary>
        /// Given a lambda expression that calls a method, returns the method info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetMethodInfo((LambdaExpression)expression);
        }

        /// <summary>
        /// Given a lambda expression that calls a method, returns the method info.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            MethodCallExpression outermostExpression = expression.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        public static void TestProperties<T>(this T objectToTest)
        {
            var properties = objectToTest.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var property in properties)
            {
                object originalValue = property.GetValue(objectToTest);
                object testValue = GetTestValue(property.PropertyType);

                // Skip properties that don't have a setter or a suitable test value
                if (testValue == null) continue;

                // Set the test value and retrieve it back
                property.SetValue(objectToTest, testValue);
                object retrievedValue = property.GetValue(objectToTest);

                // Verify the set operation was successful
                if (!Equals(testValue, retrievedValue))
                {
                    throw new AssertException($"Property test failed for '{property.Name}'. Expected value: {testValue}, Retrieved value: {retrievedValue}");
                }

                // Optionally, reset the property to its original value
                property.SetValue(objectToTest, originalValue);
            }
        }

        private static object GetTestValue(Type propertyType)
        {
            // Handle common types with predefined test values
            if (propertyType == typeof(int) || propertyType == typeof(int?)) return 123;
            if (propertyType == typeof(string)) return "TestString";
            if (propertyType == typeof(bool) || propertyType == typeof(bool?)) return true;
            if (propertyType.IsEnum) return Activator.CreateInstance(propertyType); // Use the first enum value
                                                                                    // Add more types as needed

            // For complex types, consider using mocking libraries or custom factory methods
            return null; // Return null if no suitable test value is found
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllAssembliesFromSolutionTopLevelDirectory(string combine = null)
        {
            // Get the full path to the directory containing the executing assembly.
            var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(executingAssemblyPath);

            // Assuming a standard solution structure, where the solution directory is two levels up from the bin directory.
            var solutionDirectoryPath = Path.GetFullPath(Path.Combine(directoryPath, @"..\.."));

            string topLevelBinDirectory = string.Empty;
            if (!string.IsNullOrEmpty(combine))
            {
                // Define the path to the solution's top-level bin directory (adjust as necessary).
                topLevelBinDirectory = Path.Combine(solutionDirectoryPath, combine);
            }

            // Check if the directory exists.
            if (!Directory.Exists(topLevelBinDirectory))
            {
                Trace.WriteLine("The top-level bin directory does not exist.");
                return Array.Empty<string>();
            }

            // Get all DLL files in the top-level bin directory and its subdirectories.
            var assemblyFiles = Directory.GetFiles(topLevelBinDirectory, "*.dll", SearchOption.AllDirectories);

            return assemblyFiles;
        }
    }
}
