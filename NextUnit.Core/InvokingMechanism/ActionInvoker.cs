using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.InvokingMechanism
{
	public class ActionInvoker : Invoker, IInvoker
	{
		public object Invoke(Delegate @delegate, object instance, object[] parameters)
		{
			Instance = instance;
			throw new NotImplementedException();
		}
	}
}
