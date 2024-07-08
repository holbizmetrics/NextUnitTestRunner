using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class LogTestExecutionAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            LogTestExecutionAttribute logTestExecutionAttribute = attribute as LogTestExecutionAttribute;
            logTestExecutionAttribute.BeforeTestExecution();
            logTestExecutionAttribute.AfterTestExecution();
        }
    }
}
