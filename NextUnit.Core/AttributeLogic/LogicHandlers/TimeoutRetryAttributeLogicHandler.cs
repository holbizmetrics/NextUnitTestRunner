using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;
namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeoutRetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, Delegate @delegate, object testInstance)
        {
            var timeoutRetryAttribute = attribute as TimeoutRetryAttribute;
            if (timeoutRetryAttribute != null)
            {
                int retryCount = timeoutRetryAttribute.RetryCount;
                TimeSpan timeout = timeoutRetryAttribute.Timeout;
                bool testPassed = false;

                for (int attempt = 0; attempt < retryCount && !testPassed; attempt++)
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    var task = Task.Run(() =>
                    {
                        try
                        {
                            testMethod.Invoke(testInstance, @delegate, null);
                            testPassed = true;
                        }
                        catch (TargetInvocationException ex)
                        {
                            // Handle exceptions thrown by the test method
                            // ...
                        }
                    }, cancellationTokenSource.Token);

                    if (task.Wait(timeout))
                    {
                        if (testPassed)
                        {
                            break; // Test passed, no need for further retries
                        }
                    }
                    else
                    {
                        cancellationTokenSource.Cancel();
                        // Timeout reached, proceed to next attempt
                    }
                }

                if (!testPassed)
                {
                    // Handle the case where the test failed after all retries
                    // For example, throw a custom exception or mark the test as failed
                    throw new Exception("Test failed after all retries with timeout.");
                }
            }
        }
    }
}
