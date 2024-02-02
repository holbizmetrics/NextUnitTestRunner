using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// This will repeat a test until the timeout is rfeached.
    /// </summary>
    public class ExecuteUntilTimeoutAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var executeUntilTimeoutAttribute = attribute as ExecuteUntilTimeoutAttribute;
            if (executeUntilTimeoutAttribute != null)
            {
                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed < executeUntilTimeoutAttribute.Timeout)
                {
                    testMethod.Invoke(testInstance, null);
                    Thread.Sleep(executeUntilTimeoutAttribute.Interval);
                }
                stopwatch.Stop();
            }
        }
    }
}
