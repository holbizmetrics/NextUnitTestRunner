namespace NextUnitTestRunner.TestAttributes
{
    /// <summary>
    /// Used to skip parameter check (so this only should be used for properties right now, restriction is not build in, yet)
    /// This may be not needed anymore later on.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SkipAttribute : Attribute
    {
        public string Reason { get; set; } = string.Empty;
        public SkipAttribute() { }
        public SkipAttribute(string reason)
        {
            Reason = reason;
        }
    }
}
