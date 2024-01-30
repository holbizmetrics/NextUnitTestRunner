using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RepetitionsAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RepetitionsAttribute repetitionsAttribute = attribute as RepetitionsAttribute;
            for (int i = 0; i < repetitionsAttribute.Count; i++)
            {
                testMethod.Invoke(testInstance, null);
            }
        }
    }
}
