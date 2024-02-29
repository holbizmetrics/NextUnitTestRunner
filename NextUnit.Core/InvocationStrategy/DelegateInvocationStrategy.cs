using System.Reflection;

namespace NextUnit.Core.InvocationStrategy
{
    public class DelegateInvokeStrategy : IMethodInvocationStrategy
    {
        public object Invoke(MethodInfo methodInfo, object target, params object[] parameters)
        {
            var delegateType = GetDelegateTypeForMethod(methodInfo);
            var methodDelegate = Delegate.CreateDelegate(delegateType, target, methodInfo);
            return methodDelegate.DynamicInvoke(parameters);
        }

        private Type GetDelegateTypeForMethod(MethodInfo methodInfo)
        {
            // Implementation to create a delegate type based on MethodInfo
            // This can be similar to what was discussed for the DelegateTypeFactory
            throw new NotImplementedException();
        }
    }
}
