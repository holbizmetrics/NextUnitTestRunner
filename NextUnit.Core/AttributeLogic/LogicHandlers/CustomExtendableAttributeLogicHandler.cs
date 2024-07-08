using NextUnit.Core.TestAttributes;
using NextUnit.Core.Extensions;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Enables the user to create own method attributes that can easily be used.
    /// </summary>
    public class CustomExtendableAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            CustomExtendableAttribute customExtendableAttribute = attribute as CustomExtendableAttribute;
            IEnumerable<object> returnValues = customExtendableAttribute.GetData(@delegate.GetMethodInfo());
            if (returnValues != null && returnValues.Any())
            {
                foreach (var dataItem in returnValues)
                {
					// Execute the test method with the provided data
					Invoker.Invoke(@delegate, testInstance, new object[] { dataItem }); //testMethod.Invoke(testInstance, @delegate, new object[] { dataItem });
				}
            }
            else
            {
                // Execute the test method normally if no data is provided
                // TODO: Recheck after using the new invoker.
                Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
			}
        }
    }
}
