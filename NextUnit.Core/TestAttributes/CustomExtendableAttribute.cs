using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this attribute to extend and implement own tests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomExtendableAttribute : TestAttribute
    {
    }
}
