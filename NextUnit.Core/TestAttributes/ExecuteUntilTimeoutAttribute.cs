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
            Timeout = TimeSpan.Parse(executeUntilTimeSpanExceeded);
            if(interval != null)
            {
                Interval = TimeSpan.Parse(interval);
            }
        }
    }
}