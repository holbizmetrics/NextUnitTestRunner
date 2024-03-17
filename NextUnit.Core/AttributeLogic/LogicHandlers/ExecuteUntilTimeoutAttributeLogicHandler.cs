using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// This will repeat a test until the timeout is reached.
    /// </summary>
    public class ExecuteUntilTimeoutAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var executeUntilTimeoutAttribute = attribute as ExecuteUntilTimeoutAttribute;
            if (executeUntilTimeoutAttribute != null)
            {
                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed < executeUntilTimeoutAttribute.Timeout)
                {
                    testMethod.Invoke(testInstance, @delegate, null);
                    Thread.Sleep(executeUntilTimeoutAttribute.Interval);
                }
                stopwatch.Stop();
            }
        }
    }
}
