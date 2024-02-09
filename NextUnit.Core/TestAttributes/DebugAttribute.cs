using System.Diagnostics;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Starts the debugger if not already attached.
    /// </summary>
    public class DebugAttribute : CommonDebugAttribute
    {
        /// <summary>
        /// only launches debugger if not attached AND the condition is true.
        /// </summary>
        /// <param name="condition"></param>
        public DebugAttribute(bool condition = false)
            : base(condition)
        {
        }
    }
}
