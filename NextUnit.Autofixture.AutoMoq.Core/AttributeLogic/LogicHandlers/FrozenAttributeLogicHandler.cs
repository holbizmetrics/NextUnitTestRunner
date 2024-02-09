using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class FrozenAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var frozenAttribute = attribute as FrozenAttribute;
            if (frozenAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                fixture.Customize(frozenAttribute.GetCustomization(testMethod.GetParameters().First()));

                var parameters = testMethod.GetParameters()
                                .Select(p => ResolveParameter(fixture, p))
                                .ToArray();
                testMethod.Invoke(testInstance, parameters);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
