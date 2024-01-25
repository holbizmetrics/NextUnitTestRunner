using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public RunAfterAttribute(string dateTime)
        {
            ExecuteAfter = DateTime.Parse(dateTime);
        }
    }
}
