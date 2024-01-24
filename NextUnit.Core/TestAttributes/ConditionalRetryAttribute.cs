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
        public string ConditionMethodName { get; private set; }
        public int MaxRetry { get; private set; }

        public ConditionalRetryAttribute(string conditionMethodName, int maxRetry = -1)
        {
            ConditionMethodName = conditionMethodName;
            MaxRetry = maxRetry;
        }
    }
}
