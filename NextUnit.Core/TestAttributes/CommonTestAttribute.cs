namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This marks a test in general in this framework. But in the end we use TestAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class CommonTestAttribute : Attribute
    {
    }
}
