using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestRunner.AttributeLogic
{
    public interface IAttributeLogicHandler
    {
        void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance);
    }
}