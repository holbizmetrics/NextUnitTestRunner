using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.InvokingMechanism
{
	public class ReflectionMethodInvoker : Invoker, IInvoker
	{
		private MethodInfo MethodInfo = null;
		public ReflectionMethodInvoker(MethodInfo methodInfo, object instance = null)
		{
			MethodInfo = methodInfo;
			Instance = instance;
		}
		public void Set(MethodInfo methodInfo)
		{
			MethodInfo = methodInfo;
		}

		public object Invoke(Delegate @delegate, object instance, object[] parameters = null)
		{
			Instance = instance;
			return @delegate.GetMethodInfo().Invoke(instance, parameters);
		}
	}
}
