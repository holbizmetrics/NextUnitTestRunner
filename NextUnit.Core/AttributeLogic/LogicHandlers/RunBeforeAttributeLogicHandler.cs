using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RunBeforeAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RunBeforeAttribute runBeforeAttribute = attribute as RunBeforeAttribute;
            if (DateTime.Now <= runBeforeAttribute.ExecuteBefore)
            {
                testMethod.Invoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException("RunBeforeAttribute Exception: The specified date should be <= now.");
            }
        }
    }
}
