using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class InjectDataAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            InjectDataAttribute injectDataAttribute = attribute as InjectDataAttribute;
            testMethod.Invoke(testInstance, injectDataAttribute.Parameters);
        }
    }
}
