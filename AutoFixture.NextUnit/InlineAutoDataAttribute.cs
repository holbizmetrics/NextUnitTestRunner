using AutoFixture.Kernel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// This attribute acts as a TestCaseAttribute but allows incomplete parameter values,
    /// which will be provided by AutoFixture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [CLSCompliant(false)]
    public class InlineAutoDataAttribute : Attribute//, ITestBuilder
    {
        private readonly object[] existingParameterValues;

        private readonly Lazy<IFixture> fixtureLazy;

        //private ITestMethodBuilder testMethodBuilder = new FixedNameTestMethodBuilder();

        private IFixture Fixture => fixtureLazy.Value;

        /// <summary>
        /// Gets the parameter values for the test method.
        /// </summary>
        public IEnumerable<object> Arguments => existingParameterValues;

        public object[] ExplicitArguments { get; private set; }

        public InlineAutoDataAttribute(params object[] explicitArguments)
        {
            this.ExplicitArguments = explicitArguments ?? Array.Empty<object>();
            this.fixtureLazy = new Lazy<IFixture>(() => new Fixture());
        }

        ///// <summary>
        ///// Construct a AutoFixture.NextUnit.InlineAutoDataAttribute with parameter values
        ////  for test method.
        ///// </summary>
        ///// <param name="arguments"></param>
        //public InlineAutoDataAttribute(params object[] arguments)
        //    : this(() => new Fixture(), arguments)
        //{
        //}

        /// <summary>
        /// Construct a AutoFixture.NextUnit.InlineAutoDataAttribute with an AutoFixture.IFixture
        /// and parameter values for test method.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="arguments"></param>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("This constructor overload is deprecated because it offers poor performance, and will be removed in a future version. Please use the overload with a factory method, so fixture will be constructed only if needed.")]
        protected InlineAutoDataAttribute(IFixture fixture, params object[] arguments)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }

            fixtureLazy = new Lazy<IFixture>(() => fixture, LazyThreadSafetyMode.None);
            existingParameterValues = arguments ?? new object[1];
        }

        /// <summary>
        /// Initializes a new instance of the AutoFixture.NextUnit.AutoDataAttribute class
        /// with the supplied fixtureFactory.Fixture will be created on demand using the
        /// provided factory.
        /// </summary>
        /// <param name="fixtureFactory"></param>
        /// <param name="arguments"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected InlineAutoDataAttribute(Func<IFixture> fixtureFactory, params object[] arguments)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            fixtureLazy = new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly);
            existingParameterValues = arguments ?? new object[1];
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
