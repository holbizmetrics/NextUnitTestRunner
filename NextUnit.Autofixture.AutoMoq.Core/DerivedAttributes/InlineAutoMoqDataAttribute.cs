using AutoFixture.NextUnit;

namespace NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes
{
    /// <summary>
    /// Provides a test method attribute that combines the functionality of InlineAutoDataAttribute with the Moq mocking capabilities of AutoMoqDataAttribute.
    /// This attribute is designed for scenarios where specific test data values need to be defined inline, while still automatically generating Moq mocks and random data for other parameters.
    ///
    /// The InlineAutoMoqDataAttribute is particularly useful when testing specific cases or boundary conditions with certain parameters, 
    /// but you also want to maintain the convenience of automatic mock creation for interfaces and abstract classes, as well as random data generation for other parameters.
    ///
    /// Usage:
    /// [Test, InlineAutoMoqData(42, "specific value")]
    /// public void Test(int fixedValue, string fixedString, MyClass myClass, IMyInterface mock)
    /// {
    ///     // 'fixedValue' will be 42,
    ///     // 'fixedString' will be "specific value",
    ///     // 'myClass' is an instance with random data,
    ///     // 'mock' is an automatically created Moq mock object.
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        /// <summary>
        /// This will also initialize moqs.
        /// But it won't be setting up properties.
        /// </summary>
        /// <param name="objects"></param>
        public InlineAutoMoqDataAttribute(params object[] objects)
            : base(new AutoMoqDataAttribute(), objects)
        {
        }

        /// <summary>
        /// When setupProperties = true then the additional behavior of AutoMoqDataAttribute
        /// will automatically set up the properties as well.
        /// This has been achieved by extending 
        /// The <see cref="InlineAutoMoqDataAttribute"/> 
        /// </summary>
        /// <param name="setupProperties"></param>
        /// <param name="objects"></param>
        public InlineAutoMoqDataAttribute(bool setupProperties = true, params object[] objects)
            : base(new AutoMoqDataAttribute(setupProperties), objects)
        {

        }
    }
}
