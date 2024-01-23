namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Marks a method as a method whose arguments will be auto-generated using
    /// AutoFixture during a test run.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoDataAttribute : Attribute
    {
        private readonly Lazy<IFixture> fixtureLazy;

        private IFixture Fixture => fixtureLazy.Value;

        /// <summary>
        /// Construct an AutoFixture.NextUnit.AutoDataAttribute.
        /// </summary>
        public AutoDataAttribute()
            : this(() => new Fixture())
        {
        }

        /// <summary>
        /// Construct an AutoFixture.NextUnit.AutoDataAttribute with an AutoFixture.IFixture.
        /// </summary>
        /// <param name="fixture"></param>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("This constructor overload is deprecated because it offers poor performance, and will be removed in a future version. Please use the AutoDataAttribute(Func<IFixture> fixtureFactory) overload, so fixture will be constructed only if needed.")]
        protected AutoDataAttribute(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }

            fixtureLazy = new Lazy<IFixture>(() => fixture, LazyThreadSafetyMode.None);
        }

        /// <summary>
        /// Initializes a new instance of the AutoFixture.NextUnit.AutoDataAttribute class
        /// with the supplied fixtureFactory. Fixture will be created on demand using the
        /// provided factory.
        /// 
        /// </summary>
        /// <param name="fixtureFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected AutoDataAttribute(Func<IFixture> fixtureFactory)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
        }
    }
}