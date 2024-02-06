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
        public Action<IFixture> CustomizeFixture { get; set; }
        public Action<AutoMoqCustomization> CustomizeMoq { get; set; }

        public FixturableAutoMoqDataAttribute()
            : base(() => CreateFixture(null, null))
        {
        }

        public FixturableAutoMoqDataAttribute(Action<IFixture> customizeFixture, Action<AutoMoqCustomization> customizeMoq)
            : base(() => CreateFixture(customizeFixture, customizeMoq))
        {
            CustomizeFixture = customizeFixture;
            CustomizeMoq = customizeMoq;
        }

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
