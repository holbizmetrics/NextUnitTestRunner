using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RandomAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {

            // Logic for handling CommonTestAttribute
            RandomAttribute randomAttribute = attribute as RandomAttribute;
            for (int i = 0; i < randomAttribute.ExecutionCount; i++)
            {
                testMethod.Invoke(testInstance, new object[] { randomAttribute.RandomValue });
            }
        }
    }
}
