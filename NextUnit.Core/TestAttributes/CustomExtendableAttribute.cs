using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this attribute to extend and implement own tests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class CustomExtendableAttribute : TestAttribute
    {
        public CustomExtendableAttribute()
        { 
        }

        public virtual IEnumerable<object> GetData(MethodInfo methodInfo)
        {
            return null;
        }
    }
}
