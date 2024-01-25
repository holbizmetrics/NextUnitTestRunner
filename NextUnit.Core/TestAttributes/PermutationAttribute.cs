namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// All combinations (permutated) of params in a method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermutationAttribute : CommonTestAttribute
    {
    }
}
