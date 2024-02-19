namespace NextUnit.Core.TestAttributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ValuesAttribute : Attribute
    {
        public object[] Values { get; private set; }

        public ValuesAttribute(params object[] values)
        {
            Values = values;
        }
    }
}
