using AutoFixture;
using AutoFixture.NextUnit;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class CustomizeAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            foreach (var parameter in testMethod.GetParameters())
            {
                var customizeAttribute = parameter.GetCustomAttributes()
                    .OfType<CustomizeAttribute>()
                    .FirstOrDefault();

                if (customizeAttribute != null)
                {
                    var customization = customizeAttribute.GetCustomization(parameter);
                    ApplyCustomization(customization, parameter);
                }
            }
        }

        private void ApplyCustomization(ICustomization customization, ParameterInfo parameter)
        {
            // This example uses AutoFixture, but adapt it to your test data generation framework
            var fixture = new Fixture();
            customization.Customize(fixture);

            // Generate the parameter value using the customized fixture
            //TODO: this is not finished, yet.
            //object parameterValue = fixture.Create<T>(parameter.ParameterType);

            // You need to decide how to store and use this generated parameter value
        }

    }
}
