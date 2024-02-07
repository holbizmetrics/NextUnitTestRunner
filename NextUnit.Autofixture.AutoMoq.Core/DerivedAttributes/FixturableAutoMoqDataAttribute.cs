using AutoFixture.AutoMoq;
using AutoFixture.NextUnit;
using AutoFixture;

namespace NextUnit.Autofixture.AutoMoq.Core.DerivedAttributes
{
    /// <summary>
    /// Provides a highly flexible test method attribute for automatic data generation.
    /// This attribute combines AutoFixture's data generation with Moq's mocking capabilities,
    /// and allows for extensive customization through provided configuration actions.
    ///
    /// It supports:
    /// - Automatic generation of random data for test parameters.
    /// - Automatic creation of Moq mocks for interface or abstract class parameters.
    /// - Customization of AutoFixture behavior and Moq settings via provided delegates.
    ///
    /// Usage:
    /// [Test, FlexibleAutoMoqData(CustomizeFixture = fixture => { /* Custom fixture configurations */ },
    ///                             CustomizeMoq = moq => { /* Custom Moq configurations */ })]
    /// public void TestMethod(MyClass myClass, IMyInterface mock)
    /// {
    ///     // 'myClass' is an instance with random data,
    ///     // 'mock' is a Moq mock object with potential custom setup.
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class FixturableAutoMoqDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Enables to use a customized fixture.
        /// </summary>
        public Action<IFixture> CustomizeFixture { get; set; }

        /// <summary>
        /// Enables to use a self-defined AutoMoqCustomization.
        /// </summary>
        public Action<AutoMoqCustomization> CustomizeMoq { get; set; }

        /// <summary>
        /// If no customization is needed this can be leveraged.
        /// </summary>
        public FixturableAutoMoqDataAttribute()
            : base(() => CreateFixture(null, null))
        {
        }

        /// <summary>
        /// Puts the specified customizeFixture and customizeMoq into the constructor.
        /// For more info see header description of this class.
        /// </summary>
        /// <param name="customizeFixture"></param>
        /// <param name="customizeMoq"></param>
        public FixturableAutoMoqDataAttribute(Action<IFixture> customizeFixture, Action<AutoMoqCustomization> customizeMoq)
            : base(() => CreateFixture(customizeFixture, customizeMoq))
        {
            CustomizeFixture = customizeFixture;
            CustomizeMoq = customizeMoq;
        }

        /// <summary>
        /// This is needed to be used above by the constructor.
        /// </summary>
        /// <param name="customizeFixture"></param>
        /// <param name="customizeMoq"></param>
        /// <returns></returns>
        private static IFixture CreateFixture(Action<IFixture> customizeFixture, Action<AutoMoqCustomization> customizeMoq)
        {
            var fixture = new Fixture();
            var moqCustomization = new AutoMoqCustomization();
            customizeMoq?.Invoke(moqCustomization);
            fixture.Customize(moqCustomization);
            customizeFixture?.Invoke(fixture);
            return fixture;
        }
    }
}
