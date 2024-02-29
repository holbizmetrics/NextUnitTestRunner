using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class FavorEnumerablesAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var favorEnumerablesAttribute = attribute as FavorEnumerablesAttribute;
            if (favorEnumerablesAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var constructorCustomization = new ConstructorCustomization(
                    testMethod.GetParameters().First().ParameterType,
                    new EnumerableFavoringConstructorQuery());

                fixture.Customize(constructorCustomization);

                var parameters = testMethod.GetParameters()
                                .Select(p => ResolveParameter(fixture, p))
                                .ToArray();

                testMethod.Invoke(testInstance, @delegate, parameters);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
