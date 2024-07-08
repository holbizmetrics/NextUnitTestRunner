using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using System.Reflection;
using NextUnit.Core;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class GreedyAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            GreedyAttribute greedyAttribute = attribute as GreedyAttribute;
            if (greedyAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                //fixture.Customizations.Add(new GreedyConstructorQuery());

                var parameters = @delegate.GetMethodInfo().GetParameters()
                                .Select(p => ResolveParameterWithGreedyConstructor(fixture, p))
                                .ToArray();
                Invoker.Invoke(@delegate, testInstance, parameters); //testMethod.Invoke(testInstance, @delegate, parameters);
            }
        }

        private object ResolveParameterWithGreedyConstructor(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
