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
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            CustomExtendableAttribute customExtendableAttribute = attribute as CustomExtendableAttribute;
            IEnumerable<object> returnValues = customExtendableAttribute.GetData(testMethod);
            if (returnValues != null && returnValues.Any())
            {
                foreach (var dataItem in returnValues)
                {
                    // Execute the test method with the provided data
                    testMethod.Invoke(testInstance, @delegate, new object[] { dataItem });
                }
            }
            else
            {
                // Execute the test method normally if no data is provided
                testMethod.Invoke(testInstance, @delegate, null);
            }
        }
    }
}
