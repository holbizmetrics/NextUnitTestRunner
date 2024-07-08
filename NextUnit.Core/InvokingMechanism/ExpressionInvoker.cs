using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.InvokingMechanism
{
	public class ExpressionInvoker : Invoker, IInvoker
	{
		public ExpressionInvoker() 
		{
		}

		public object Invoke(Delegate @delegate, object instance, object[] parameters)
		{
			throw new NotImplementedException();
		}
	}
}