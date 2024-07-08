using System.Reflection;

namespace NextUnit.Core.AttributeLogic
{
    public interface IAttributeLogicHandler
    {
        void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance);
    }
}