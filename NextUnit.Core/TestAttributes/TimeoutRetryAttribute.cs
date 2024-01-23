namespace NextUnit.Core.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
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
