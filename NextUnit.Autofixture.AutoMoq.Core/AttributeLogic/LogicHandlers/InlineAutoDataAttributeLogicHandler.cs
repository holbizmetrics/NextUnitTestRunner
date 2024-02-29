using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.Extensions;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    public class InlineAutoDataAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var inlineAutoDataAttribute = attribute as InlineAutoDataAttribute;
            if (inlineAutoDataAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var parameters = testMethod.GetParameters();
                var arguments = new object[parameters.Length];

                // Use explicit arguments provided by InlineAutoDataAttribute
                for (int i = 0; i < inlineAutoDataAttribute.ExplicitArguments.Length; i++)
                {
                    arguments[i] = inlineAutoDataAttribute.ExplicitArguments[i];
                }

                // Generate remaining arguments using AutoFixture
                for (int i = inlineAutoDataAttribute.ExplicitArguments.Length; i < parameters.Length; i++)
                {
                    arguments[i] = ResolveParameter(fixture, parameters[i]);
                }

                testMethod.Invoke(testInstance, @delegate, arguments);
            }
        }

        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            return new SpecimenContext(fixture).Resolve(parameterInfo);
        }
    }
}
