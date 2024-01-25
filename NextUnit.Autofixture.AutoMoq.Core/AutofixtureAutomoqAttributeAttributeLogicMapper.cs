using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core
{
    public class AutofixtureAutomoqAttributeAttributeLogicMapper : AttributeLogicMapper
    {
        public AutofixtureAutomoqAttributeAttributeLogicMapper()
        {
            _mapping.Add(typeof(AutoDataAttribute), new AutoDataAttributeLogicHandler());
            _mapping.Add(typeof(InlineAutoDataAttribute), new InlineAutoDataAttributeLogicHandler());
        }

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

        public class InlineAutoDataAttributeLogicHandler : IAttributeLogicHandler
        {
            public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
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

                    testMethod.Invoke(testInstance, arguments);
                }
            }

            private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
            {
                return new SpecimenContext(fixture).Resolve(parameterInfo);
            }
        }
    }
}
