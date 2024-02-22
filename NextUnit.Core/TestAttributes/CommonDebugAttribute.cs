using System.Diagnostics;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This attribute is used for the DebuggerLaunchAttribute and DebuggerBreakAttribute to either launch or break.
    /// </summary>
    public abstract class CommonDebugAttribute : CommonTestAttribute
    {
        public bool Condition { get; set; } = false;
        public CommonDebugAttribute(bool condition)
        {
            Condition = condition;
        }

        /// <summary>
        /// This attaches the debugger if not already attached and the condition is true.
        /// </summary>
        public virtual void Debug()
        {
            if (!Condition || Debugger.IsAttached)
            {
                return;
            }
            Debugger.Launch();
        }

        /// <summary>
        /// This signals a breakpoint to the debugger.
        /// </summary>
        public virtual void Break()
        {
            Debugger.Break();
        }
    }
}
