using System.Reflection;

namespace NextUnitTestRunner.Extensions
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
    }
}
