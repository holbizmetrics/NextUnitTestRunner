using System;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This will only run when the certain test time has been exceeded.
    /// So, in other words, this test will start to work after the specified time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RunAfterAttribute : CommonTestAttribute
    {
        public DateTime ExecuteAfter { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="dateTime"></param>
        public RunAfterAttribute(string dateTime)
        {
            Init(dateTime);
        }

        /// <summary>
        /// If no DateTime is set we set the current time
        /// </summary>
        public RunAfterAttribute()
            : this(DateTime.Now.ToString())
        {
        }

        private void Init(string dateTime)
        {
            DateTime dateTimeExecuteAfter;
            bool success = DateTime.TryParse(dateTime, out dateTimeExecuteAfter);
            if (!success)
            {
                return;
            }
            ExecuteAfter = dateTimeExecuteAfter;
        }

    }
}
