namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Use this to categorize, respectively group tests.
    /// The equivalent in NUnit may be the Category Attribute.
    /// And it would be great if I figure out how to make those categories then appear in the Test Explorer in Visual Studio as well.
    /// I guess the NextUnit.TestAdapter would have to be made working correctly to support this as well?!
    /// 
    /// For the moment it is allowed to apply multiple GroupAttributes to a method. Let's see if we'll keep it like this.
    /// Because as far as I understood until now the Test Explorer in Visual Studio should support this as well (untested for now).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GroupAttribute : CommonTestAttribute
    {
        public bool AutomaticNaming = false;
        public string GroupName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// This should be sufficient if you only want to specify the name.
        /// </summary>
        /// <param name="name"></param>
        public GroupAttribute(string name) 
            : this(name, string.Empty)
        {
        }

        /// <summary>
        /// Use this to set the name AND the value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public GroupAttribute(string name, string value)
        {
            GroupName = name;
            Value = value;
        }

        /// <summary>
        /// If automatic naming is set the Group attribute, respectively the handler tries to 
        /// derive the name itself from being put over a method.
        /// </summary>
        /// <param name="automaticNaming"></param>
        public GroupAttribute(bool automaticNaming)
        {
            AutomaticNaming = automaticNaming;
        }
    }
}
