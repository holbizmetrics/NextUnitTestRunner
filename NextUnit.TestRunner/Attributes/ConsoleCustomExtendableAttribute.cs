using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestRunner.Attributes
{
    public class ConsoleCustomExtendableAttribute : CustomExtendableAttribute
    {
        public ConsoleCustomExtendableAttribute()
        {
            Console.WriteLine($"(Hello from {this.GetType()}: Hallo -------------------------------------------------------------------------------");
        }
    }
}
