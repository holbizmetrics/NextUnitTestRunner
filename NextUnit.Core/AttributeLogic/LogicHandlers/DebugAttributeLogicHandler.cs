using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Handles debug/break controlled by DebugAttribute.
    /// </summary>
    public class DebugAttributeLogicHandler : IAttributeLogicHandler
    {
        /// <summary>
        /// If true we'll create a Debugger.Launch(), if false it will be a Debugger.Break().
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="testMethod"></param>
        /// <param name="delegate"></param>
        /// <param name="testInstance"></param>
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
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
