using AutoFixture;
using AutoFixture.AutoMoq;

namespace NextUnit.TestRunnerTests.AdditionalAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AutoMoqDataNextUnit : AutoFixture.NextUnit.AutoDataAttribute
    {
        public AutoMoqDataNextUnit()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
