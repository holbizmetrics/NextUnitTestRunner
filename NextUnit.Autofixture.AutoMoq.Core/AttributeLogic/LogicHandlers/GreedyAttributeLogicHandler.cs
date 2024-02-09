using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class GreedyAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            GreedyAttribute greedyAttribute = attribute as GreedyAttribute;
            if (greedyAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                //fixture.Customizations.Add(new GreedyConstructorQuery());

                var parameters = testMethod.GetParameters()
                                .Select(p => ResolveParameterWithGreedyConstructor(fixture, p))
                                .ToArray();
                testMethod.Invoke(testInstance, parameters);
            }
        }

        private object ResolveParameterWithGreedyConstructor(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
