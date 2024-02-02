namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This can be used to mark parameter properties of an attribute as such together with the function
    /// Together with GetMarkedAttributeValues
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NextUnitValueAttribute: Attribute
    {
    }
}