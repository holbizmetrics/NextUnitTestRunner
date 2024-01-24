using System;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Generate random values for a test by using this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RandomAttribute : CommonTestAttribute
    {
        protected int seedChange = 0;
        public int Min { get; }
        public int Max { get; }

        public int ExecutionCount { get; }

        public RandomAttribute(int min, int max, int executionCount)
        {
            Min = min;
            Max = max;
            ExecutionCount = executionCount;

            seedChange += DateTime.Now.Millisecond;
        }

        public RandomAttribute(int min, int max)
            : this(min, max, 1)
        {
        }

        public int RandomValue
        {  
            get 
            { 
                Random random = new Random(DateTime.Now.Millisecond + seedChange);
                return random.Next(Min, Max + 1);
            }
        }
    }
}
