namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to repeat a test it it fails.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RetryAttribute : CommonTestAttribute
    {
        public int RetryCount { get; set; } = 1;
        public RetryAttribute()
        {
        }

        public RetryAttribute(int retryCount)
        {
            RetryCount = retryCount;
        }
    }
}
