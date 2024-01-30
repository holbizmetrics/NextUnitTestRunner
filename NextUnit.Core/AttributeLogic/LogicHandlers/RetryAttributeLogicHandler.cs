using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var retryAttribute = attribute as RetryAttribute;
            if (retryAttribute != null)
            {
                int retryCount = retryAttribute.RetryCount;
                bool testPassed = false;

                for (int attempt = 0; attempt < retryCount && !testPassed; attempt++)
                {
                    try
                    {
                        testMethod.Invoke(testInstance, null);
                        testPassed = true; // If no exception is thrown, the test passed
                    }
                    catch (TargetInvocationException ex)
                    {
                        if (attempt == retryCount - 1)
                        {
                            // Last attempt, rethrow the exception or handle it as a test failure
                            throw; // Or handle as a test failure
                        }
                        // Log or handle the failure of this attempt, if necessary
                    }
                }

                if (!testPassed)
                {
                    // Handle the case where the test failed after all retries
                    // E.g., mark the test as failed in the test results
                }
            }
        }
    }
}
