namespace NextUnit.Core.Asserts
{
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}