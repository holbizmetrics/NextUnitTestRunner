using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class InlineAutoDataAttributeTests
    {
        [Test]
        [InlineAutoData]
        public void InlineAutoDataAttributeTest(Address address)
        {
            Assert.IsInstanceOfType(address, typeof(InlineAutoDataAttribute));
        }

        public class Address
        {
            public string Street { get; set; } 
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }

            public Address()
            {
            }
        }
    }
}
