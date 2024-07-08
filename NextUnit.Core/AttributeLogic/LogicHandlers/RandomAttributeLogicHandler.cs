using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RandomAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RandomAttribute randomAttribute = attribute as RandomAttribute;
            for (int i = 0; i < randomAttribute.ExecutionCount; i++)
            {
                Invoker.Invoke(@delegate, testInstance, new object[] { randomAttribute.RandomValue }); //testMethod.Invoke(testInstance, @delegate, new object[] { randomAttribute.RandomValue });
			}
        }
    }
}
