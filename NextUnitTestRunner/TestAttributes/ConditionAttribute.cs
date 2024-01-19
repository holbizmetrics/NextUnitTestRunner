namespace NextUnitTestRunner.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionAttribute : CommonTestAttribute
    {
        public bool Condition { get; set; } = false;
        public ConditionAttribute(bool condition)
        {

        }
    }
}
