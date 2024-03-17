namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this attribute to check if other tests already passed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DependentOnAttribute : Attribute
    {
        public bool LocalTestsOnly = true;
        public List<string> MethodNames { get; private set; } = new List<string>();
        /// <summary>
        /// If local tests only is true then this only works for the tests where this test is marked with the DependentOnAttribute
        /// </summary>
        /// <param name="localTestsOnly"></param>
        /// <param name="methodNames"></param>
        public DependentOnAttribute(bool localTestsOnly = true, params string[] methodNames)
        {
            this.LocalTestsOnly = localTestsOnly;
            this.MethodNames = MethodNames;
        }

        public DependentOnAttribute(params string[] methodNames)
            : this(true, methodNames)
        {

        }
    }
}