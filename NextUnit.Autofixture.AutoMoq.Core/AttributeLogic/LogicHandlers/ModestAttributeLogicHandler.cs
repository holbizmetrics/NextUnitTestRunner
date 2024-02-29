using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class ModestAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var modestAttribute = attribute as ModestAttribute;
            if (modestAttribute != null)
            {
                // Create an AutoFixture fixture and customize it with AutoMoq
                var fixture = new Fixture().Customize(new AutoMoqCustomization());

                // Apply the customization for each parameter
                foreach (var param in testMethod.GetParameters())
                {
                    // Check if the parameter has ModestAttribute
                    if (param.GetCustomAttribute(typeof(ModestAttribute)) != null)
                    {
                        // Apply customization to use modest constructor for this parameter type
                        fixture.Customize(new ConstructorCustomization(param.ParameterType, new ModestConstructorQuery()));
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