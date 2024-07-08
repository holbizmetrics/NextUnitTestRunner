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
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
        {
            var executeUntilTimeoutAttribute = attribute as ExecuteUntilTimeoutAttribute;
            if (executeUntilTimeoutAttribute != null)
            {
                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed < executeUntilTimeoutAttribute.Timeout)
                {
                    Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
                    Thread.Sleep(executeUntilTimeoutAttribute.Interval);
                }
                stopwatch.Stop();
            }
        }
    }
}
