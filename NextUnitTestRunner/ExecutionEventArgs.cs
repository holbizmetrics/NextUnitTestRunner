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

        public Exception LastException { get; internal set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public ExecutionEventArgs()
        {
        }

        public ExecutionEventArgs(MethodInfo methodInfo, TestResult testResult, Exception exception = null)
            : this(methodInfo)
        {
            TestResult = testResult;
            LastException = exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public ExecutionEventArgs(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }

        public override string ToString()
        {
            string exceptionText = 
$@"MethodInfo: {MethodInfo}
TestResult: {TestResult}";
            if (LastException!=null)
            {
                exceptionText += LastException;
            }
            return exceptionText;
        }
    }

    public delegate void ExecutionEventHandler(object sender, ExecutionEventArgs e);
}
