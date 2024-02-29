using System.Reflection;

namespace NextUnit.Core.InvocationStrategy
{
    public class MethodInvocationContext
    {
        private readonly IMethodInvocationStrategy _strategy;

        public MethodInvocationContext(IMethodInvocationStrategy strategy)
        {
            _strategy = strategy;
        }

        public object InvokeMethod(MethodInfo methodInfo, object target, params object[] parameters)
        {
            return _strategy.Invoke(methodInfo, target, parameters);
        }
    }
}
