using System.Diagnostics;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Signals a break
    /// </summary>
    public class DebuggerBreakAttribute : CommonDebugAttribute
    {
        public DebuggerBreakAttribute(bool checkIfDebuggerIsAttached = false)
            :base(checkIfDebuggerIsAttached)
        {
            bool signalDebugBreakPoint = checkIfDebuggerIsAttached ? Debugger.IsAttached : true;
            if (!signalDebugBreakPoint) { return; }
        }
    }
}
