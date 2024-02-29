using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NextUnit;
using AutoFixture;
using NextUnit.Core.AttributeLogic;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoDataAttributeLogicHandler : IAttributeLogicHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="testMethod"></param>
        /// <param name="testInstance"></param>
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var autoDataAttribute = attribute as AutoDataAttribute;
            if (autoDataAttribute != null)
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var parameters = testMethod.GetParameters()
                                .Select(p => ResolveParameter(fixture, p))
                                .ToArray();

                // TODO: this definitely prevents a very strange error. But is this correct like this? Check!
                // It may make sense to look up what the behavior should be, like in other frameworks.
                if (parameters.Length > 0)
                {
                    testMethod.Invoke(testInstance, @delegate, parameters);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="parameterInfo"></param>
        /// <returns></returns>
        private object ResolveParameter(IFixture fixture, ParameterInfo parameterInfo)
        {
            // Ensure the correct type is being passed
            return fixture.Create(parameterInfo.ParameterType, new SpecimenContext(fixture));
        }
    }
}
