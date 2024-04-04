using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Attributes
{
    public class TimeoutAttributeTests
    {/// <summary><
     /// This test has to fail if it takes longer then n to execute. [Timeout(n)]
     /// </summary>
        [Timeout(1000)]
        public void Timeout()
        {

        }
    }
}