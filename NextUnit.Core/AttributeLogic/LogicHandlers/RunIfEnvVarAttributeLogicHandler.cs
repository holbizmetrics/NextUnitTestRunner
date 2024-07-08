using NextUnit.Core.AttributeLogic;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RunIfEnvVarAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            RunIfEnvVarAttribute runIfEnvVarAttribute = attribute as RunIfEnvVarAttribute;
            var envValue = Environment.GetEnvironmentVariable(runIfEnvVarAttribute.VariableName);
            if (envValue != runIfEnvVarAttribute.ExpectedValue)
            {
                
                //test.RunState = RunState.Skipped;
                //test.Properties.Set(PropertyNames.SkipReason, $"Test skipped because environment variable '{variableName}' does not equal '{expectedValue}'.");
            }
        }
    }
}
