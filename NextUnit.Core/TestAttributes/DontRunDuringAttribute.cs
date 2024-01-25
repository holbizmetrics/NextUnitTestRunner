using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Determines an interval when a test will be not executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DontRunDuringAttribute : RunDuringAttribute
    {
        public DontRunDuringAttribute(string begin, string end)
            : base(begin, end)
        {
        }
    }
}
