using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NextUnit;

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
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
