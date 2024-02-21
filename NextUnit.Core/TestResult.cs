namespace NextUnit.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum ExecutionState
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
        /// Added to indicate as well if a test case may have been stuck.
        /// </summary>
        Running,

        /// <summary>
        /// Other unknown error, due to framework bugs, possibly, etc. (for now)
        /// </summary>
        UnknownError,
    }

    /// <summary>
    /// 
    /// </summary>
    public class TestResult
    {
        public static TestResult Empty
        {
            get
            {
                return new TestResult
                {
                    DisplayName = string.Empty,
                    State = ExecutionState.NotStarted,
                    StackTrace = string.Empty,
                    Start = DateTime.MinValue, // or some other appropriate default value
                    End = DateTime.MinValue, // or some other appropriate default value
                    ExecutionTime = TimeSpan.Zero,
                    Workstation = string.Empty,
                    Class = string.Empty,
                    Namespace = string.Empty,
                    Exception = null
                };
            }
        }

        public string DisplayName { get; set; } = string.Empty;
        public ExecutionState State { get; set; } = ExecutionState.NotStarted;

        public DateTime CreationTime { get; } = DateTime.Now;
        public string StackTrace { get; set; } = string.Empty;

        /// <summary>
        /// When the test was started.
        /// </summary>
        public DateTime Start { get; set; }
        
        /// <summary>
        /// When the test finished.
        /// </summary>
        public DateTime End { get; set; }
        public TimeSpan ExecutionTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// On which machine we are running.
        /// </summary>
        public string Workstation { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Namespace { get; set; } = string.Empty;
        public Exception Exception { get; set; } = null;

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
