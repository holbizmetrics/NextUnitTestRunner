namespace NextUnit.Core.TestAttributes
{
    public class RunIfEnvVarAttribute : CommonTestAttribute
    {
        public readonly string VariableName;
        public readonly string ExpectedValue;

        public RunIfEnvVarAttribute(string variableName, string expectedValue)
        {
            this.VariableName = variableName;
            this.ExpectedValue = expectedValue;
        }
    }
}
