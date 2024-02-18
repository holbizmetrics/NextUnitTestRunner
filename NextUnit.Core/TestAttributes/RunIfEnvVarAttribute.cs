using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NextUnit.Core.TestAttributes
{
    public class RunIfEnvVarAttribute : CommonTestAttribute
    {
        public readonly string VariableName;
        public readonly string ExpectedValue;

        public RunIfEnvVarAttribute(string variableName, string expectedValue)
        {
            this.VariableName = variableName;
            this.ExpectedValue = expectedValue;
        }
    }
}
