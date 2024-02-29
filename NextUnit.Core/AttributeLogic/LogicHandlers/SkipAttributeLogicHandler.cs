using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class SkipAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod,Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            //This is easy. Just do nothing in terms of executing. :-)
        }
    }
}
