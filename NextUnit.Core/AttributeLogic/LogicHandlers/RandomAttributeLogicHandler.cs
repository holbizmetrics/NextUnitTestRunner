using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RandomAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {

            // Logic for handling CommonTestAttribute
            RandomAttribute randomAttribute = attribute as RandomAttribute;
            for (int i = 0; i < randomAttribute.ExecutionCount; i++)
            {
                testMethod.Invoke(testInstance, @delegate, new object[] { randomAttribute.RandomValue });
            }
        }
    }
}
