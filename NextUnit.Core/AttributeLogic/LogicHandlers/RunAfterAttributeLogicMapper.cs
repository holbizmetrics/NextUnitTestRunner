using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RunAfterAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            // Logic for handling CommonTestAttribute
            RunAfterAttribute runAfterAttribute = attribute as RunAfterAttribute;
            if (DateTime.Now >= runAfterAttribute.ExecuteAfter)
            {
                @delegate.DynamicInvoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException("RunAfterAttribute Exception");
            }
        }
    }
}
