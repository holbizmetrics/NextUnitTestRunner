namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Basically the same as in other frameworks some attributes that may be called DataRow, Inline, etc.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InjectDataAttribute : CommonTestAttribute, IParameter
    {
        public object[] Parameters { get; private set; }

        //public InjectDataAttribute() { }
        public InjectDataAttribute(params object[] args)
        {
            Parameters = args;
        }

        public object[] GetParameters()
        {
            return Parameters;
        }
    }
}
