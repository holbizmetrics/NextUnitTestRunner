using System.Security;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This can be used to compile source code.
    /// Be aware that this can slow down the TestRun immensively 
    /// </summary>
    [SecurityCritical(SecurityCriticalScope.Explicit)]
    public class CompileAttribute : CommonTestAttribute
    {
        public bool UseFile { get; set; } = false;
        public string Source { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public CompileAttribute(string source, bool useFile = true, string methodName = null)
        { 
            Source = source;
            UseFile = useFile;
        }
    }
}
