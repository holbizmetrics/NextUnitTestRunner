﻿using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
	/// <summary>
	/// This will cause the method to be run in a thread.
	/// </summary>
	public class RunInThreadAttributeLogicHandler : IAttributeLogicHandler
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="attribute"></param>
		/// <param name="testMethod"></param>
		/// <param name="testInstance"></param>
		public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
		{
			RunInThreadAttribute runInThreadAttribute = attribute as RunInThreadAttribute;
			Thread thread = new Thread(() => { Invoker.Invoke(@delegate, testInstance,	null); /*testMethod.Invoke(testInstance, @delegate, null);*/ });
			thread.ApartmentState = runInThreadAttribute.ApartmentState;
			thread.IsBackground = runInThreadAttribute.IsBackground;
			if (runInThreadAttribute.CultureInfo != null)
			{
				thread.CurrentCulture = runInThreadAttribute.CultureInfo;
			}
			thread.Start();
			if (runInThreadAttribute.AddJoin)
			{
				thread.Join();
			}
			// Logic for handling CommonTestAttribute
		}
	}
}
