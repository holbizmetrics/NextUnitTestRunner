﻿using NextUnit.Core.TestAttributes;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    public class RetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, Delegate @delegate, object testInstance)
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
                        Invoker.Invoke(@delegate, testInstance, null); //testMethod.Invoke(testInstance, @delegate, null);
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
