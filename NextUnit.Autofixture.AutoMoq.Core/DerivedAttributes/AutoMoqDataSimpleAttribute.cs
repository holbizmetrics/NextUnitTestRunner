using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixture.NextUnit.DerivedAttributes
{
    /// <summary>
    /// Provides a test method attribute that enhances AutoFixture's data generation with 
    /// advanced Moq mocking capabilities.
    /// 
    /// This attribute extends the functionality of the standard AutoData attribute.
    /// It's possible to use it to automatically specifying random values.
    /// 
    /// but also allowing for more customized behavior of Moq-generated mock objects for interfaces or abstract class parameters.
    ///
    /// We can also 
    /// The attribute supports additional customization of the AutoFixture behavior, enabling fine-tuning of the test data generation process.
    /// This is particularly useful for complex testing scenarios where specific configurations of mocks or data are required.
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AutoMoqDataSimpleAttribute : AutoDataAttribute
    {
        public AutoMoqDataSimpleAttribute(bool configureMembers = true, bool generateDelegates = true)
            : base(() => new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = configureMembers,
                GenerateDelegates = generateDelegates
            }))
        {
        }
    }
}
