namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Only execute if condition is being fulfilled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionAttribute : CommonTestAttribute
    {
        public string ConditionMethodName { get; private set; }

        public ConditionAttribute(string conditionMethodName)
        {
            ConditionMethodName = conditionMethodName ?? throw new ArgumentNullException(nameof(conditionMethodName));
        }
    }
}
