namespace NextUnitTestRunner.TestAttributes
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
    [AttributeUsage(AttributeTargets.Method)]
    public class Repetitions : CommonTestAttribute
    {
        public int Count { get; set; } = -1;
        public Repetitions()
        {

        }

        public Repetitions(int count)
        {
            Count = count;
        }
    }

}
