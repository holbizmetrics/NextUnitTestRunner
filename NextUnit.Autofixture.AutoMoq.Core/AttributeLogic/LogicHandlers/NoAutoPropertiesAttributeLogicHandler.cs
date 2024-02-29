using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class NoAutoPropertiesAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate  @delegate, object testInstance)
        {
            var noAutoPropertiesAttribute = attribute as NoAutoPropertiesAttribute;
            if (noAutoPropertiesAttribute != null)
            {
                // Create an AutoFixture fixture and customize it with AutoMoq
                var fixture = new Fixture().Customize(new AutoMoqCustomization());

                // Apply the customization for each parameter
                foreach (var param in testMethod.GetParameters())
                {
                    // Check if the parameter has NoAutoPropertiesAttribute
                    if (param.GetCustomAttribute(typeof(NoAutoPropertiesAttribute)) != null)
                    {
                        // Apply customization to disable auto-property filling for this parameter type
                        fixture.Customize(new NoAutoPropertiesCustomization(param.ParameterType));
                    }
                }

                // Resolve parameters and invoke the test method
                var parameters = testMethod.GetParameters().Select(p => ResolveParameter(fixture, p)).ToArray();
                testMethod.Invoke(testInstance, @delegate, parameters);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            // Ensure the correct type is being passed
            return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
        }
    }
}
