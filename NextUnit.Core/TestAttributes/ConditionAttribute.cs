namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// Only execute if condition is being fulfilled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionAttribute : CommonTestAttribute
    {
        public string ConditionDelegateName { get; set; } = string.Empty;
        public bool Condition { get; set; } = false;
        public ConditionAttribute(bool condition)
        {
            Condition = condition;
        }

        public ConditionAttribute(DateTime dateTime)
        {

        }
        public ConditionAttribute(bool condition, string conditionDelegateName)
            :this(condition)
        {

        }

        public ConditionAttribute(bool condition, Func<bool> conditionDelegate)
        {

        }
    }
}
