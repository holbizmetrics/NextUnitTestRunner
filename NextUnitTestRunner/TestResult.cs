namespace NextUnit.TestRunner
{
    /// <summary>
    /// 
    /// </summary>
    public enum ExecutedState
    {
        /// <summary>
        /// Test hasn't been started.
        /// </summary>
        NotStarted,

        /// <summary>
        /// Test passed
        /// </summary>
        Passed,

        /// <summary>
        /// Test failed
        /// </summary>
        Failed,

        /// <summary>
        /// Test skipped
        /// </summary>
        Skipped,

        /// <summary>
        /// Test wasn't found.
        /// </summary>
        NotFound,

        /// <summary>
        /// Other unknown error, due to framework bugs, possibly, etc. (for now)
        /// </summary>
        UnknownError
    }

    /// <summary>
    /// 
    /// </summary>
    public class TestResult
    {
        public string DisplayName { get; internal set; } = string.Empty;
        public ExecutedState State { get; internal set; } = ExecutedState.NotStarted;

        public string StackTrace { get; internal set; } = string.Empty;

        /// <summary>
        /// When the test was started.
        /// </summary>
        public DateTime Start { get; internal set; }
        
        /// <summary>
        /// When the test finished.
        /// </summary>
        public DateTime End { get; internal set; }
        public TimeSpan ExecutionTime { get; internal set; } = TimeSpan.Zero;

        /// <summary>
        /// On which machine we are running.
        /// </summary>
        public string Workstation { get; internal set; } = string.Empty;
        public string Class { get; internal set; } = string.Empty;
        public string Namespace { get; internal set; } = string.Empty;
        public Exception Exception { get; internal set; } = null;

        public override string ToString()
        {
            string result = 
$@"DisplayName: {DisplayName}
Class: {Class}, Namespace: {Namespace}
Start: {Start}
End: {End}
Execution Time: {ExecutionTime}
Workstation: {Workstation}
State: {State}
";
            return result;
        }
    }
}
