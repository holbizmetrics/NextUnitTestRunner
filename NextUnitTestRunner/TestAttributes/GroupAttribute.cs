namespace NextUnitTestRunner.TestAttributes
{
    /// <summary>
    /// Use this to categorize, respectively group tests.
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
