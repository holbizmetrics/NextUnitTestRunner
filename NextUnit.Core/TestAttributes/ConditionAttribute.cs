namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Only execute if condition is being fulfilled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionAttribute : CommonTestAttribute
    {
        public bool Condition { get; set; } = false;
        public ConditionAttribute(bool condition)
        {
            Condition = condition;
        }
    }
}
