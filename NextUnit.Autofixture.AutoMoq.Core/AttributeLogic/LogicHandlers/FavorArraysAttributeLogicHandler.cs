using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;
using NextUnit.Core.Extensions;
using NextUnit.Core;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class FavorArraysAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var favorArraysAttribute = attribute as FavorArraysAttribute;
            if (favorArraysAttribute == null) return;

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize(new ConstructorCustomization(
                @delegate.GetMethodInfo().DeclaringType,
                new ArrayFavoringConstructorQuery()));

            var parameters = @delegate.GetMethodInfo().GetParameters();
            var arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                arguments[i] = ResolveParameter(fixture, parameters[i]);
            }

            Invoker.Invoke(@delegate, testInstance, arguments); // testMethod.Invoke(testInstance, @delegate, arguments);
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            // Resolve the parameter based on its type, applying the ArrayFavoringConstructorQuery customization
            return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
        }
    }
}
