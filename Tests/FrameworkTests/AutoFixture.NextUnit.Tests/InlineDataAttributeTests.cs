using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class InlineDataAttributeTests
    {
        [Test]
        [InlineData(1)]
        public void InlineDataAttributeTest(int param1)
        {
            Assert.AreEqual(1, param1);
        }
    }
}
