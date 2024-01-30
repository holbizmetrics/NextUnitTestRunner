namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to execute a test until the timeout timespan has been exceeded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ExecuteUntilTimeoutAttribute : CommonTestAttribute
    {
        public TimeSpan Timeout { get; set; } = TimeSpan.Zero;
        public TimeSpan Interval { get; private set; } = TimeSpan.FromSeconds(5);
        public ExecuteUntilTimeoutAttribute()
        { 
        }

        public ExecuteUntilTimeoutAttribute(string executeUntilTimeSpanExceeded, string interval = null)
        {
            if (!TimeSpan.TryParse(executeUntilTimeSpanExceeded, out TimeSpan parsedTimeout))
            {
                // Handle parse failure, e.g., set to default value, throw exception, etc.
                // Example: Setting to default TimeSpan.Zero
                parsedTimeout = TimeSpan.Zero;
            }
            Timeout = parsedTimeout;

            if (interval != null && TimeSpan.TryParse(interval, out TimeSpan parsedInterval))
            {
                Interval = parsedInterval;
            }
            // Optionally handle the case where interval parsing fails
        }
    }
}