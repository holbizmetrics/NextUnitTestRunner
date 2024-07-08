using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Use this for Categories.
    /// </summary>
    public class GroupAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            // Logic for handling GroupAttribute
            // We shouldn't need to be doing anything here for now, because this is "just" used to
            // make a category visible in the TestExplorer.
        }
    }

}
