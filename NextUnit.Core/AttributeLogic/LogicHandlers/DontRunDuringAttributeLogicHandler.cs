using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Use this to not run a test during a certain time interval.
    /// Thus, it has the same effect as the SkippedAttribute, basically.
    /// </summary>
    public class DontRunDuringAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            // Logic for handling CommonTestAttribute

            DateTime now = DateTime.Now;

            DontRunDuringAttribute dontRunDuringAttribute = attribute as DontRunDuringAttribute;
            if (now < dontRunDuringAttribute.Begin || now > dontRunDuringAttribute.End)
            {
                testMethod.Invoke(testInstance, @delegate, null);
            }
            else
            {
                throw new ExecutionEngineException("DontRunDuringAttribute Exception");
            }
        }
    }
}
