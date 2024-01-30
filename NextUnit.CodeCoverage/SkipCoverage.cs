namespace NextUnit.CodeCoverage
{
    /// <summary>
    /// If this is used above one of the below specified entitites this will not be considered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Assembly | AttributeTargets.Interface | AttributeTargets.Constructor | AttributeTargets.Struct)]
    public class SkipCoverageAttribute : Attribute
    {
    }
}