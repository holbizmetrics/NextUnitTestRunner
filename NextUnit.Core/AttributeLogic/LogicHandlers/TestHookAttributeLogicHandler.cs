using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// This will (for now) provide two hook points to run something BEFORE or AFTER a test.
    /// </summary>
    public class TestHookAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            TestHookAttribute testHookAttribute = attribute as TestHookAttribute;
        }
    }
}
