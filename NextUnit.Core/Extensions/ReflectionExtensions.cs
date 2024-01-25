using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace NextUnit.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static Type GetInterface<T>(this Type type)
        {
            return type.GetInterfaces().FirstOrDefault(x => x == typeof(T));
        }

        public static T GetValue<T>(this Type type, string name)
        {
            return GetValue<T>(type, name, type);
        }

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
            if (field!=null)
            {
                returnValue = field.GetValue(instance);
                return (T)returnValue;
            }

            return default(T);
        }

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

        public static bool IsComparable(this Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type);
        }

        public static bool IsEquatable(this Type type)
        {
            // Construct the specific type of IEquatable<T> where T is the parameter type
            Type equatableType = typeof(IEquatable<>).MakeGenericType(type);
            bool isEquatable = equatableType.IsAssignableFrom(type);
            return isEquatable;
        }

        public static Type FindNestedTypeContainingInterface(this Type type)
        {
            //type.GetNestedType().GetInterface();
            return typeof(void);
        }
    }
}
