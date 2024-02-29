using System.Reflection;

namespace NextUnit.Core.InvocationStrategy
{
    public interface IMethodInvocationStrategy
    {
        object Invoke(MethodInfo methodInfo, object target, params object[] parameters);
    }
}
