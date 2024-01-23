namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// If timeout execution exceeded test fails.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TimeoutAttribute : CommonTestAttribute
    {
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(1);
        
        public TimeoutAttribute()
        {
        }

        public TimeoutAttribute(int milliseconds)
        {
            Timeout = TimeSpan.FromMilliseconds(milliseconds);
        }

        public TimeoutAttribute(TimeSpan timeout)
        { 
            Timeout = timeout;
        }
    }
}
