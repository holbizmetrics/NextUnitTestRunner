using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// This will be checking if the other methods this is dependent on have been successfully passed.
    /// Otherwise, if one of the tests given in DependentOn("Test1", "Test2", "Test3") has failed,
    /// this test marked with this attribute will fail as well.
    /// </summary>
    public class DependentOnAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            DependentOnAttribute dependentOnAttribute = attribute as DependentOnAttribute;
        }
    }
}
