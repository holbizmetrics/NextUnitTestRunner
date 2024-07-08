﻿using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// This is used to handle the ConditionAttribute which takes a bool delegate (specified by a name as a string)
    /// and executes it in the method.
    /// </summary>
    public class ConditionAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var conditionAttribute = attribute as ConditionAttribute;
            if (conditionAttribute != null)
            {
                var conditionMethod = testInstance.GetType().GetMethod(conditionAttribute.ConditionMethodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                Type returnType = conditionMethod.ReturnType;
                if (returnType != typeof(bool))
                {
                    throw new ExecutionEngineException($"{@delegate.GetMethodInfo()}: ConditionAttribute condition method has wrong return type. Should be bool but was {returnType}");
                }
                if (conditionMethod != null && (bool)conditionMethod.Invoke(testInstance, null))
                {
                    Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
                }
                else
                {
                    // Handle the case when the condition method is not found, or the condition is false.
                }
            }
        }
    }
}
