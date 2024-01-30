using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyInjectionAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var dependencyInjectionAttribute = attribute as DependencyInjectionAttribute;
            if (dependencyInjectionAttribute != null)
            {
                var getNestedInterfaceImplementation = testInstance.GetType().GetNestedTypes().Where(x => x.GetInterface(dependencyInjectionAttribute.ServiceType.Name) != null).First();
                object createObjectWhereInterfaceExistsIn = Activator.CreateInstance(getNestedInterfaceImplementation);

                if (createObjectWhereInterfaceExistsIn != null)
                {
                    var parameters = new object[] { createObjectWhereInterfaceExistsIn };
                    testMethod.Invoke(testInstance, parameters);
                }
            }
        }
    }

}
