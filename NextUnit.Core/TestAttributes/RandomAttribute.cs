namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Generate random values for a test by using this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RandomAttribute : CommonTestAttribute, IParameter
    {
        private int seedChange = 0;
        public int Min { get; }
        public int Max { get; }

        public int ExecutionCount { get; }

        public RandomAttribute(int min, int max, int executionCount)
        {
            Min = min;
            Max = max;
            ExecutionCount = executionCount;
        }

        public object[] GetParameters()
        {
            seedChange += DateTime.Now.Millisecond;
            Random random = new Random(DateTime.Now.Millisecond + seedChange);
            object[] args = new object[] { random.Next(Min, Max + 1), random.Next(Min, Max + 1) };
            return args;
        }
    }
}
