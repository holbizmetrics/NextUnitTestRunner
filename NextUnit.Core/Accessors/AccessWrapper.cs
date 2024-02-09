using System.Collections;
using System.Dynamic;
using System.Reflection;

namespace NextUnit.Core.Accessors
{
    /// <summary>
    /// Usage example:
    /// 
    /// var example = new ExampleClass();
    /// var wrapper = new AccessWrapper(example);
    /// dynamic dynamicExample = wrapper.AsDynamic();
    /// 
    /// Access private field and property
    /// 
    /// Console.WriteLine(dynamicExample._privateField); // Outputs: 10
    /// Console.WriteLine(dynamicExample.PrivateProperty); // Outputs: 20
    /// Modify private field and property
    /// dynamicExample._privateField = 30;
    /// dynamicExample.PrivateProperty = 40;
    /// Console.WriteLine(dynamicExample._privateField); // Outputs: 30
    /// Console.WriteLine(dynamicExample.PrivateProperty); // Outputs: 40
    //
    // Call private method
    //
    // dynamicExample.PrivateMethod(); // Outputs: "Called PrivateMethod"
    /// </summary>
    public class AccessWrapper
    {
        private readonly object _target;
        private readonly Type _targetType;

        public AccessWrapper(object target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _targetType = target.GetType();
        }

        public object InvokeMethod(string methodName, params object[] parameters)
        {
            MethodInfo method = _targetType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (method == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found on type '{_targetType.FullName}'.");
            }
            return method.Invoke(_target, parameters);
        }

        public object GetPropertyOrField(string name, int? index = null)
        {
            object value = GetPropertyOrFieldValue(name);
            if (index.HasValue)
            {
                if (value is Array array)
                {
                    return array.GetValue(index.Value);
                }
                else if (TryGetIndexedValue(value, index.Value, out object indexedValue))
                {
                    return indexedValue;
                }
                else
                {
                    throw new InvalidOperationException($"The member '{name}' does not support indexed access.");
                }
            }
            return value;
        }

        public void SetPropertyOrField(string name, object value, int? index = null)
        {
            if (index.HasValue)
            {
                object target = GetPropertyOrFieldValue(name);
                if (target is Array arr)
                {
                    arr.SetValue(value, index.Value);
                }
                else if (TrySetIndexedValue(target, index.Value, value))
                {
                    // Success
                }
                else
                {
                    throw new InvalidOperationException($"The member '{name}' does not support indexed access.");
                }
            }
            else
            {
                SetPropertyOrFieldValue(name, value);
            }
        }

        private object GetPropertyOrFieldValue(string name)
        {
            PropertyInfo propInfo = _targetType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (propInfo != null)
            {
                return propInfo.GetValue(_target, null);
            }

            FieldInfo fieldInfo = _targetType.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(_target);
            }

            throw new InvalidOperationException($"Property or field '{name}' not found on type '{_targetType.FullName}'.");
        }

        private void SetPropertyOrFieldValue(string name, object value)
        {
            PropertyInfo propInfo = _targetType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (propInfo != null)
            {
                propInfo.SetValue(_target, value, null);
                return;
            }

            FieldInfo fieldInfo = _targetType.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(_target, value);
                return;
            }

            throw new InvalidOperationException($"Property or field '{name}' not found on type '{_targetType.FullName}'.");
        }

        // Utility methods to get or set indexed value for generic collections
        private bool TryGetIndexedValue(object target, int index, out object value)
        {
            if (target is IList list)
            {
                value = list[index];
                return true;
            }
            else if (target.GetType().IsGenericType && target is IEnumerable enumerable)
            {
                Type iListType = typeof(IList<>).MakeGenericType(target.GetType().GetGenericArguments()[0]);
                if (iListType.IsAssignableFrom(target.GetType()))
                {
                    value = iListType.GetProperty("Item").GetValue(target, new object[] { index });
                    return true;
                }
            }

            value = null;
            return false;
        }

        private bool TrySetIndexedValue(object target, int index, object value)
        {
            if (target is IList list)
            {
                list[index] = value;
                return true;
            }
            else if (target.GetType().IsGenericType && target is IEnumerable enumerable)
            {
                Type iListType = typeof(IList<>).MakeGenericType(target.GetType().GetGenericArguments()[0]);
                if (iListType.IsAssignableFrom(target.GetType()))
                {
                    iListType.GetProperty("Item").SetValue(target, value, new object[] { index });
                    return true;
                }
            }

            return false;
        }

        public dynamic AsDynamic()
        {
            return new DynamicAccessProxy(_target);
        }
    }
}
