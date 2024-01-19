namespace NextUnitTestRunner.TestAttributes
{
    /// <summary>
    /// Use this to execute a test until the timeout timespan has been exceeded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecuteUntilTimeoutAttribute : CommonTestAttribute
    {
        public TimeSpan ExecuteUntilTimeSpan { get; set; } = TimeSpan.Zero;
        public ExecuteUntilTimeoutAttribute()
        { 
        }

        public ExecuteUntilTimeoutAttribute(TimeSpan executeUntilTimeSpan)
        {

        }
    }
}