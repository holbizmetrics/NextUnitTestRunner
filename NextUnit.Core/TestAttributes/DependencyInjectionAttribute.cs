namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// 
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
