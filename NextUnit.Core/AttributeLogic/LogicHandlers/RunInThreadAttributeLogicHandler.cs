using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RunInThreadAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            Thread thread = new Thread(() => { testMethod.Invoke(testInstance, null); });
            thread.Start();
            thread.Join();
            // Logic for handling CommonTestAttribute
        }
    }
}
