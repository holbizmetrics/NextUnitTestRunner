namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to categorize, respectively group tests.
    /// The equivalent in NUnit may be the Category Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GroupAttribute : CommonTestAttribute
    {
        public string GroupName { get; set; } = string.Empty;
        public GroupAttribute() { }
        public GroupAttribute(string name)
        {
            GroupName = name;
        }
    }
}
