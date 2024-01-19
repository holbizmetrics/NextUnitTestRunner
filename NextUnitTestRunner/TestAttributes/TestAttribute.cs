namespace NextUnitTestRunner.TestAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : CommonTestAttribute
{
    public string AliasName { get; set; }
    public string Description { get; set; }

    public TestAttribute()
    {
    }
    public TestAttribute(string aliasName)
    {

    }

    public TestAttribute(string aliasName, string description)
        : this(aliasName)
    {
        Description = description;
    }
}

public class DataAttribute : CommonTestAttribute
{
}
