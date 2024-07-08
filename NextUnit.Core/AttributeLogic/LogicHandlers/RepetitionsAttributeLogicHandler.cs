using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RepetitionsAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RepetitionsAttribute repetitionsAttribute = attribute as RepetitionsAttribute;
            for (int i = 0; i < repetitionsAttribute.Count; i++)
            {
                Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
            }
        }
    }
}
