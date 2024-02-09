using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class FavorArraysAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var favorArraysAttribute = attribute as FavorArraysAttribute;
            if (favorArraysAttribute == null) return;

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize(new ConstructorCustomization(
                testMethod.DeclaringType,
                new ArrayFavoringConstructorQuery()));

            var parameters = testMethod.GetParameters();
            var arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                arguments[i] = ResolveParameter(fixture, parameters[i]);
            }

            testMethod.Invoke(testInstance, arguments);
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            // Resolve the parameter based on its type, applying the ArrayFavoringConstructorQuery customization
            return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
        }
    }
}
