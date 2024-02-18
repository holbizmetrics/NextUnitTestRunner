namespace NextUnit.Core.Asserts
{
    /// <summary>
    /// General type for definining Assert Exceptions.
    /// </summary>
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}