using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to fuzz a test, respectively parameters.
    /// 
    /// in the first level this will just detect the parameters of the test method and execute them with 
    /// Max/Min for the IConvertible/IEquatable automatically.
    /// </summary>
    public class FuzzingAttribute : CommonTestAttribute
    {
    }
}
