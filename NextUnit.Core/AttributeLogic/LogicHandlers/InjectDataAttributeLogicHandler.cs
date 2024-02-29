using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class InjectDataAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            InjectDataAttribute injectDataAttribute = attribute as InjectDataAttribute;
            testMethod.Invoke(testInstance, @delegate, injectDataAttribute.Parameters);
        }
    }
}
