using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
	public class RunDuringAttributeLogicHandler : IAttributeLogicHandler
	{
		public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
		{
			// Logic for handling CommonTestAttribute
			// Logic for handling CommonTestAttribute
			RunDuringAttribute runDuringAttribute = attribute as RunDuringAttribute;
			DateTime now = DateTime.Now;

			// Check if the current time is within the begin and end range
			if (now >= runDuringAttribute.Begin && now <= runDuringAttribute.End)
			{
				testMethod.Invoke(testInstance, @delegate, null);
			}
			else
			{
				throw new ExecutionEngineException("RunDuringAttribute Exception");
			}
		}
	}
}
