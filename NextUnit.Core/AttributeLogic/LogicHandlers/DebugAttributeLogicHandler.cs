using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class DebugAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            CommonDebugAttribute debug = attribute as CommonDebugAttribute;
            if (debug.GetType() == typeof(DebugAttribute))
            {
                debug.Debug();
            }
            else if(debug.GetType() == typeof(DebuggerBreakAttribute)) 
            {
                debug.Break();
            }
        }
    }
}
