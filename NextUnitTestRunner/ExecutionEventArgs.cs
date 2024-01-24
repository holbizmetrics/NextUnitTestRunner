using System.Reflection;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionEventArgs : EventArgs
    {
        public MethodInfo MethodInfo { get; set; } = null;
        public TestResult TestResult { get; private set; }
        public string Message { get; internal set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ExecutionEventArgs()
        {
        }

        public ExecutionEventArgs(MethodInfo methodInfo, TestResult testResult)
            : this(methodInfo)
        {
            TestResult = testResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public ExecutionEventArgs(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }
    }

    public delegate void ExecutionEventHandler(object sender, ExecutionEventArgs e);
}
