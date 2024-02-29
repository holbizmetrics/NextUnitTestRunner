using NextUnit.Core.TestAttributes;
using NextUnit.Core.Extensions;
using System.Reflection;


namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionalRetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var conditionalRetryAttribute = attribute as ConditionalRetryAttribute;
            MethodInfo conditionMethod = testInstance.GetType().GetMethod(conditionalRetryAttribute.ConditionMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic); ;
            if (conditionMethod == null)
            {
                throw new InvalidOperationException("Condition method not found.");
            }

            int attempts = 0;
            bool conditionMet = false;

            while (attempts < conditionalRetryAttribute.MaxRetry || conditionalRetryAttribute.MaxRetry == -1)
            {
                testMethod.Invoke(testInstance, @delegate, null);

                conditionMet = (bool)conditionMethod.Invoke(testInstance, null);
                if (conditionMet)
                    break;

                attempts++;
            }

            if (!conditionMet)
            {
                // Handle the test as failed after all retries
            }
        }
    }
}
