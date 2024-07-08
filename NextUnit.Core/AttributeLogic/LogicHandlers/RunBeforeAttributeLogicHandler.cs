using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RunBeforeAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RunBeforeAttribute runBeforeAttribute = attribute as RunBeforeAttribute;
            if (DateTime.Now <= runBeforeAttribute.ExecuteBefore)
            {
                Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
            }
            else
            {
                throw new ExecutionEngineException("RunBeforeAttribute Exception: The specified date should be <= now.");
            }
        }
    }
}
