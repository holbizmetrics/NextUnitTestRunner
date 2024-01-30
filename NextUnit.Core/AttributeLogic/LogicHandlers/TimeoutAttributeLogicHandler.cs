using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class TimeoutAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var timeoutAttribute = attribute as TimeoutAttribute;
            if (timeoutAttribute != null)
            {
                var timeout = timeoutAttribute.Timeout;
                var cancellationTokenSource = new CancellationTokenSource();
                var task = Task.Run(() =>
                {
                    try
                    {
                        testMethod.Invoke(testInstance, null);
                    }
                    catch (TargetInvocationException ex)
                    {
                        // Handle exceptions thrown by the test method
                        // ...
                    }
                }, cancellationTokenSource.Token);

                if (!task.Wait(timeout))
                {
                    cancellationTokenSource.Cancel();
                    // Handle timeout exceeded
                    // For example, throw a custom timeout exception or mark the test as failed due to timeout
                    throw new TimeoutException($"Test exceeded the time limit of {timeout.TotalMilliseconds} milliseconds.");
                }
            }
        }
    }
}
