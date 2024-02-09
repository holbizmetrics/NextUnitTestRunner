using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class AutoDataAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var autoDataAttribute = attribute as AutoDataAttribute;
            if (autoDataAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var parameters = testMethod.GetParameters()
                                .Select(p => ResolveParameter(fixture, p))
                                .ToArray();
                testMethod.Invoke(testInstance, parameters);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            // Ensure the correct type is being passed
            return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
        }
    }
}
