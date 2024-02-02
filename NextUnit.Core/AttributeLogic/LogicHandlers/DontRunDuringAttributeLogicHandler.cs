using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Use this to not run a test during a certain time interval.
    /// Thus, it has the same effect as the SkippedAttribute, basically.
    /// </summary>
    public class DontRunDuring
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            // Logic for handling CommonTestAttribute

            DateTime now = DateTime.Now;

            DontRunDuringAttribute dontRunDuringAttribute = attribute as DontRunDuringAttribute;
            if (now < dontRunDuringAttribute.Begin || now > dontRunDuringAttribute.End)
            {
                testMethod.Invoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException("DontRunDuringAttribute Exception");
            }
        }
    }

}
