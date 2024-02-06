using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Resources;
using System.Runtime.CompilerServices;

namespace NextUnit.Core.Extensions
{
    public static class ReflectionExtensions
    {
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
            foreach(PropertyInfo propertyInfo in propertyInfos)
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
    }
}
