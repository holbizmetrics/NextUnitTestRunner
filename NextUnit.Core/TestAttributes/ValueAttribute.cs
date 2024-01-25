using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ValuesAttribute : Attribute
    {
        public object[] Values { get; private set; }

        public ValuesAttribute(params object[] values)
        {
            Values = values;
        }
    }
}
