namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this for dependency injection for the test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DependencyInjectionAttribute : Attribute
    {
        public Type ServiceType { get; private set; }

        public DependencyInjectionAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
