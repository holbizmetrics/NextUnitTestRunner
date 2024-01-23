namespace NextUnit.Core.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectDataAttribute : CommonTestAttribute, IParameter
    {
        public object[] Parameters { get; private set; }
        public InjectDataAttribute() { }
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
