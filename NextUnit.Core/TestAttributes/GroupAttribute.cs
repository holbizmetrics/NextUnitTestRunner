namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to categorize, respectively group tests.
    /// The equivalent in NUnit may be the Category Attribute.
    /// And it would be great if I figure out how to make those categories then appear in the Test Explorer in Visual Studio as well.
    /// I guess the NextUnit.TestAdapter would have to be made working correctly to support this as well?!
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
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
