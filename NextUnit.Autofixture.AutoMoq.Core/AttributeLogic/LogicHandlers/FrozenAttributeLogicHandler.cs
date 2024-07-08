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
    /// <summary>
    /// 
    /// </summary>
    public class FrozenAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var frozenAttribute = attribute as FrozenAttribute;
            if (frozenAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                fixture.Customize(frozenAttribute.GetCustomization(@delegate.GetMethodInfo().GetParameters().First()));

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
