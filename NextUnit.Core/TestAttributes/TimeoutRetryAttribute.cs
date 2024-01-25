namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Repeat until either the timeSpan is exceeded or the retryCount has been reached.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TimeoutRetryAttribute : Attribute
    {
        public int RetryCount { get; private set; }
        public TimeSpan Timeout { get; private set; }

        public TimeoutRetryAttribute(int retryCount, TimeSpan timeout)
        {
            RetryCount = retryCount;
            Timeout = timeout;
        }

        // Implementation of retry with timeout logic
    }
}
