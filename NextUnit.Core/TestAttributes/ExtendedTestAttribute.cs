using System.Diagnostics;

namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This is just an example for now.
    /// The testframework should(!) enable the user to either use own attributes to a certain extent.
    /// At least it should be easy to extend the framework, if possible.
    /// I don't really know how to do this, yet.
    /// But I want that. :-)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExtendedTestAttribute : TestAttribute
    {
        public object[] Values { get; set; } = null;
        public ExtendedTestAttribute(params object[] values)
        {
            Values = values;
        }
    }
}
