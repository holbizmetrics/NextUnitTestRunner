using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var conditionAttribute = attribute as ConditionAttribute;
            if (conditionAttribute != null)
            {
                var conditionMethod = testInstance.GetType().GetMethod(conditionAttribute.ConditionMethodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                Type returnType = conditionMethod.ReturnType;
                if (returnType != typeof(bool))
                {
                    throw new ExecutionEngineException($"{testMethod}: ConditionAttribute condition method has wrong return type. Should be bool but was {returnType}");
                }
                if (conditionMethod != null && (bool)conditionMethod.Invoke(testInstance, null))
                {
                    testMethod.Invoke(testInstance, null);
                }
                else
                {
                    // Handle the case when the condition method is not found, or the condition is false.
                }
            }
        }
    }
}
