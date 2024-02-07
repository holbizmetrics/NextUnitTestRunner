using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NextUnit;
using NextUnit.Autofixture.AutoMoq.Core.Customizations;

namespace NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes
{
    /// <summary>
    /// Provides a test method attribute that combines AutoFixture's data generation with Moq's mocking capabilities.
    /// AutoMoqDataAttribute automatically supplies both random data for simple parameters and Moq-generated mock objects 
    /// for interface or abstract class parameters.
    /// 
    /// This simplifies tests by reducing manual mock setup and 
    /// providing a diverse range of test data for more robust testing.
    /// 
    /// Usage:
    /// [Test, AutoMoqDatat]
    /// public void Test(IMyInterface mock, string b) 
    /// {
    ///     // 'mock' is an automatically created Moq mock object,
    ///     // 'b' is a string with random data.
    /// }
    /// </summary>    
    /// <summary>
    /// Provides an test method attribute that combines AutoFixture's data generation with Moq's mocking capabilities.

    /// Unlike the standard AutoData attribute, which only supplies random data for test method parameters, 
    /// AutoMoqDataAttribute automatically supplies both random data for simple parameters and Moq-generated mock objects 
    /// for interface or abstract class parameters.
    /// 
    /// This simplifies tests by reducing manual mock setup and 
    /// providing a diverse range of test data for more robust testing.
    /// 
    /// Usage:
    /// [Test, AutoMoqData]
    /// public void Test(IMyInterface mock, string b) 
    /// {
    ///     // 'mock' is an automatically created Moq mock object,
    ///     // 'b' is a string with random data.
    /// }
    /// </summary>    [AttributeUsage(AttributeTargets.Method)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(bool setupProperties = false)
            : base(() => CreateFixture(setupProperties))
        {
        }

        /// <summary>
        /// If the constructor above is given a 
        /// setupProperties = true
        /// it enables the attribute to fill all property values
        /// otherwise the "plain" AutoMoqCustomization is being used.
        /// </summary>
        /// <param name="setupProperties"></param>
        /// <returns></returns>
        private static IFixture CreateFixture(bool setupProperties)
        {
            var fixture = new Fixture();
            if (setupProperties)
            {
                fixture.Customize(new PropertiesCustomization());
            }
            else
            {
                fixture.Customize(new AutoMoqCustomization());
            }

            return fixture;
        }
    }
}
