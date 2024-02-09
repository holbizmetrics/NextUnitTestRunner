using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.Accessors
{
    internal class DynamicAccessProxy : DynamicObject
    {
        private readonly object _target;
        private readonly Type _targetType;

        public DynamicAccessProxy(object target)
        {
            _target = target;
            _targetType = target.GetType();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            MethodInfo method = _targetType.GetMethod(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                result = method.Invoke(_target, args);
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            PropertyInfo prop = _targetType.GetProperty(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                result = prop.GetValue(_target, null);
                return true;
            }

            FieldInfo field = _targetType.GetField(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                result = field.GetValue(_target);
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            PropertyInfo prop = _targetType.GetProperty(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                prop.SetValue(_target, value, null);
                return true;
            }

            FieldInfo field = _targetType.GetField(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(_target, value);
                return true;
            }

            return base.TrySetMember(binder, value);
        }
    }
}
