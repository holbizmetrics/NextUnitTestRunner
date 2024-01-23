namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to re-execute a test until the condition is fulfilled.
    /// Use MaxRetry to set a count how many times this should be tried.
    /// -1 will try endlessly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionalRetryAttribute : CommonTestAttribute
    {
        public int MaxRetry { get; set; } = -1;
        public bool Condition { get; set; } = false;
        public ConditionalRetryAttribute() 
        { 
        }

        public ConditionalRetryAttribute(bool condition, int maxRetry = -1)
        {
            Condition = condition;
            MaxRetry = maxRetry;
        }
    }
}
