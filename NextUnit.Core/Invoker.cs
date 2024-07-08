using NextUnit.Core.InvokingMechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core
{
	public static class Invoker
	{
		private static IInvoker UsedInvoker { get; } = new DelegateInvoker();
		public static object Invoke(Delegate @delegate, object instance, params object[] args)
		{
			return UsedInvoker.Invoke(@delegate, instance, args);
		}
	}
}
