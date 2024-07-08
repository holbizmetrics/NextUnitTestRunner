using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.InvokingMechanism
{
	public class DelegateInvoker : Invoker, IInvoker
	{
		private Delegate Delegate = null;

		public DelegateInvoker()
		{

		}

		public DelegateInvoker(Delegate @delegate)
		{
			Delegate = @delegate;
		}

		public void Set(Delegate @delegate)
		{
			Delegate = @delegate;
		}

		public object Invoke(Delegate @delegate, object instance, object[] parameters = null)
		{
			Delegate = @delegate;
			Instance = instance;
			return Delegate.DynamicInvoke(parameters);
		}
	}
}
