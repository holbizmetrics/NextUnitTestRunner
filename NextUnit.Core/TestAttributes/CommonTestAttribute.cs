namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// This marks a test in general in this framework. But in the end we use TestAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class CommonTestAttribute : Attribute, ICommonTestAttribute
    {
    }

    //TODO:
    /// <summary>
    /// We may be using this to detect test attributes.
    /// 
    /// Before fitting in AutoFixture/AutoMoq attributes this was sufficient because every attribute
    /// could inherit from CommonTestAttribute.
    /// 
    /// BUT other attributes may be derived from something else. Thus, we could search for the interface now.
    /// So, for the moment it would have no other purpose than that.
    /// 
    /// Which also means that tests for those attributes won't work right now.
    /// Not because they generally won't work, no. They work very well already.
    /// But because the detection is wrong now.
    /// </summary>
    public interface ICommonTestAttribute
    {

    }
}
