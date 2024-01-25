namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to repeat a test n times.
    /// 
    /// Example:
    /// 
    /// [Repetitions(2)]
    /// [Test]
    /// void TestTheTest()
    /// {
    /// }
    /// 
    /// will execute the TestTheTest test 2 times.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RepetitionsAttribute : CommonTestAttribute
    {
        public int Count { get; set; } = 1;
        public RepetitionsAttribute()
        {

        }

        public RepetitionsAttribute(int count)
        {
            Count = count;
        }
    }
}
