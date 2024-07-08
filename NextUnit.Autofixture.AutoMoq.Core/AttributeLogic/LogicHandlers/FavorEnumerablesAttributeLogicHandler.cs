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
    public class FavorEnumerablesAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var favorEnumerablesAttribute = attribute as FavorEnumerablesAttribute;
            if (favorEnumerablesAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var constructorCustomization = new ConstructorCustomization(
                    @delegate.GetMethodInfo().GetParameters().First().ParameterType,
                    new EnumerableFavoringConstructorQuery());

                fixture.Customize(constructorCustomization);

                var parameters = @delegate.GetMethodInfo().GetParameters()
                                .Select(p => ResolveParameter(fixture, p))
                                .ToArray();

                Invoker.Invoke(@delegate, testInstance, parameters); //testMethod.Invoke(testInstance, @delegate, parameters);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
