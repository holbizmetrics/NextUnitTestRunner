using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Determines an interval when test is being executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RunDuringAttribute : CommonTestAttribute
    {
        public DateTime Begin{ get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;

        public RunDuringAttribute(string begin, string end)
        {
            Begin = DateTime.Parse(begin);
            End = DateTime.Parse(end);
        }
    }
}
