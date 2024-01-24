using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This will only be executed if the concrete dateTime specified has not been exceeded, yet.
    /// So, in other words this test will not be executed anymore, then.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RunBeforeAttribute : CommonTestAttribute
    {
        public DateTime ExecuteBefore { get; private set; }

        public RunBeforeAttribute(string dateTime)
        {
            ExecuteBefore = DateTime.Parse(dateTime);
        }
    }
}
