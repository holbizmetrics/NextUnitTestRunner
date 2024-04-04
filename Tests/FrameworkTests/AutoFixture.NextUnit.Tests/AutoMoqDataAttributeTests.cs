using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes;
using NextUnit.Core.TestAttributes;

namespace AutoFixture.NextUnit.Tests
{
    public class AutoMoqDataAttributeTests
    {
        /// <summary>
        /// This will not be executed in the current version.
        /// Thus, we wouldn't hit a breakpoint here for the method, because there are no parameters.
        /// Only for the attribute processing OF the method.
        /// </summary>
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_NoParametersInMethodTest()
        {

        }

        /// <summary>
        /// Simple parameters means here: Just for example IComparable, IEquatable types like: int, float, double, etc. and other default types like string, etc.
        /// </summary>
        /// <param name=""></param>
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_SimpleParametersInMethodTest(int param1, int param2, int param3, string text)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(text));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_ComplexParametersInMethodTest(NestedComplexClass nestedComplexClass)
        {
            Assert.IsNotNull(nestedComplexClass);
            Assert.IsNotNull(nestedComplexClass.Address);
            Assert.IsTrue(!string.IsNullOrEmpty(nestedComplexClass.Address.Country));
            Assert.IsTrue(!string.IsNullOrEmpty(nestedComplexClass.Address.City));
            Assert.IsTrue(!string.IsNullOrEmpty(nestedComplexClass.Address.Phone));
            Assert.IsTrue(!string.IsNullOrEmpty(nestedComplexClass.Address.Street));
            Assert.IsTrue(!string.IsNullOrEmpty(nestedComplexClass.Address.PostalCode));
        }


        [Test]
        [AutoMoqData]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_OnlyMockInterfaceInMethodTest(Mock<INestedComplexClass> mockedNestedComplexClass)
        { 
            Assert.IsNotNull(mockedNestedComplexClass);
            Assert.IsNotNull(mockedNestedComplexClass.Object);
            Assert.IsNotNull(mockedNestedComplexClass.Object.Address);
        }

        [Test]
        [AutoMoqData(true)]
        [Group(nameof(AutoMoqDataAttribute))]
        public void AutoMoqDataAttribute_MockInterfaceAndComplexParametersInMethod_SetupProperties_Test(Mock<INestedComplexClass> mockedNestedComplexClassByInterfaceOnly, Mock<NestedComplexClass> mockNestedComplexClass)
        {
            // Test interfaced.
            Assert.IsNotNull(mockedNestedComplexClassByInterfaceOnly);
            Assert.IsNotNull(mockedNestedComplexClassByInterfaceOnly.Object);
            Assert.IsNotNull(mockedNestedComplexClassByInterfaceOnly.Object.Address);

            // Test non-interfaced.
            Assert.IsNotNull(mockNestedComplexClass);
            Assert.IsNotNull(mockNestedComplexClass.Object);
            Assert.IsNotNull(mockNestedComplexClass.Object.Address);
        }

        [Test]
        [Group(nameof(InlineAutoMoqDataAttribute))]
        [InlineAutoMoqData(3, 4)]
        [InlineAutoMoqData(33, 44)]
        [InlineAutoMoqData(13, 14)]
        public void SomeUnitTest(int DataFrom, int OtherData, [Frozen] Mock<ISomeInterface> theInterface, NestedComplexClass sut)
        {
        }
    }

    public interface ISomeInterface
    {

    }

    public class NestedComplexClass : INestedComplexClass
    {
        public NestedComplexClass()
        {
        }

        public Address Address { get; set; } = new Address();
    }

    public interface INestedComplexClass
    {
        Address Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
