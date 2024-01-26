using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Autofixture.AutoMoq.Core
{
    /// <summary>
    /// Extends the log of the AttributeLogicMapper to also suppoert AutoMoq.Autofixture features.
    /// </summary>
    public class AutofixtureAutomoqAttributeAttributeLogicMapper : AttributeLogicMapper
    {
        public AutofixtureAutomoqAttributeAttributeLogicMapper()
        {
            _mapping.Add(typeof(AutoDataAttribute), new AutoDataAttributeLogicHandler());
            _mapping.Add(typeof(FavorArraysAttribute), new FavorArraysAttributeLogicHandler());
            _mapping.Add(typeof(FavorEnumerablesAttribute), new FavorEnumerablesAttributeLogicHandler());
            _mapping.Add(typeof(FrozenAttribute), new FrozenAttributeLogicHandler());
            _mapping.Add(typeof(InlineAutoDataAttribute), new InlineAutoDataAttributeLogicHandler());
            _mapping.Add(typeof(GreedyAttribute), new GreedyAttributeLogicHandler());
            _mapping.Add(typeof(ModestAttribute), new ModestAttributeLogicHandler());
            _mapping.Add(typeof(NoAutoPropertiesAttribute), new NoAutoPropertiesAttributeLogicHandler());
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

        public class FavorEnumerablesAttributeLogicHandler : IAttributeLogicHandler
        {
            public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
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

                    testMethod.Invoke(testInstance, parameters);
                }
            }

            private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
            {
                return new SpecimenContext(fixture).Resolve(parameterInfo);
            }
        }

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

        public class ModestAttributeLogicHandler : IAttributeLogicHandler
        {
            public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
            {
                var modestAttribute = attribute as ModestAttribute;
                if (modestAttribute != null)
                {
                    // Create an AutoFixture fixture and customize it with AutoMoq
                    var fixture = new Fixture().Customize(new AutoMoqCustomization());

                    // Apply the customization for each parameter
                    foreach (var param in testMethod.GetParameters())
                    {
                        // Check if the parameter has ModestAttribute
                        if (param.GetCustomAttribute(typeof(ModestAttribute)) != null)
                        {
                            // Apply customization to use modest constructor for this parameter type
                            fixture.Customize(new ConstructorCustomization(param.ParameterType, new ModestConstructorQuery()));
                        }
                    }

                    // Resolve parameters and invoke the test method
                    var parameters = testMethod.GetParameters().Select(p => ResolveParameter(fixture, p)).ToArray();
                    testMethod.Invoke(testInstance, parameters);
                }
            }

            private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
            {
                // Ensure the correct type is being passed
                return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
            }
        }

        public class NoAutoPropertiesAttributeLogicHandler : IAttributeLogicHandler
        {
            public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
            {
                var noAutoPropertiesAttribute = attribute as NoAutoPropertiesAttribute;
                if (noAutoPropertiesAttribute != null)
                {
                    // Create an AutoFixture fixture and customize it with AutoMoq
                    var fixture = new Fixture().Customize(new AutoMoqCustomization());

                    // Apply the customization for each parameter
                    foreach (var param in testMethod.GetParameters())
                    {
                        // Check if the parameter has NoAutoPropertiesAttribute
                        if (param.GetCustomAttribute(typeof(NoAutoPropertiesAttribute)) != null)
                        {
                            // Apply customization to disable auto-property filling for this parameter type
                            fixture.Customize(new NoAutoPropertiesCustomization(param.ParameterType));
                        }
                    }

                    // Resolve parameters and invoke the test method
                    var parameters = testMethod.GetParameters().Select(p => ResolveParameter(fixture, p)).ToArray();
                    testMethod.Invoke(testInstance, parameters);
                }
            }

            private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
            {
                // Ensure the correct type is being passed
                return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
            }
        }
    }
}
