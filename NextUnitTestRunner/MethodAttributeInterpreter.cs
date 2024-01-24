using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// This is only kept for now because of the TestRunner, TestRunner2.
    /// </summary>
    [Obsolete]
    public class MethodAttributeInterpreter
    {
        public static object[] Interpret(CommonTestAttribute attribute)
        {
            if (attribute == null) return null;
            //Read the parameters if an interface IParameter exists in the current attribute by calling GetParameters method.
            return GetParameters(attribute);
        }

        public static object[] GetParameters(Attribute attribute)
        {
            Type IParameterInterface = attribute.GetType().GetInterface<IParameter>();
            MethodInfo methodInfo = attribute.GetType().GetMethod("GetParameters");
            if (methodInfo == null) return null;
            object parameters = methodInfo.Invoke(attribute, null);

            if (parameters is Array)
            {
                return (object[])parameters;
            }
            return null;
        }
    }
}


//ParameterInfo[] parameterInfo = method.GetParameters();
//if (args.Length != parameterInfo.Length)
//{
//    Trace.WriteLine($"{method}: Parameter count mismatch.");
//    continue;
//}

//List<object> temporaryParameters = new List<object>();
//foreach(PropertyInfo propertyInfo in randomAttribute.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
//{
//    if (propertyInfo.GetCustomAttribute<SkipAttribute>()!=null)
//    {
//        continue;
//    }
//    temporaryParameters.Add(propertyInfo.GetValue(randomAttribute, null));
//}
//parameters = temporaryParameters.ToArray();
//parameters = new object[parameterInfo.Length];
//parameters[0] = args[0];
//parameters[1] = args[1];
