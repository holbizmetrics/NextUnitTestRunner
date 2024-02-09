using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixture.NextUnit.Tests
{
    public class InlineDataAttributeTests
    {
        [Test]
        [InlineData(1)]
        public void InlineDataAttributeTest()
        {

        }
    }
}
