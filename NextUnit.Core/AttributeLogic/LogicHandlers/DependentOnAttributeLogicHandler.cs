using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class DependentOnAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            DependentOnAttribute dependentOnAttribute = attribute as DependentOnAttribute;
        }
    }
}
