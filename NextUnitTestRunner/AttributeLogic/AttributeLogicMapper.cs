using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NextUnit.TestRunner.AttributeLogic
{
    public class AttributeLogicMapper
    {
        private readonly Dictionary<Type, IAttributeLogicHandler> _mapping;

        public AttributeLogicMapper()
        {
            _mapping = new Dictionary<Type, IAttributeLogicHandler>
            {
                //{ typeof(CommonTestAttribute), new CommonTestAttributeLogicHandler() }, //Is this even needed?
                { typeof(ConditionalRetryAttribute), new ConditionalRetryAttributeLogicHandler() },
                { typeof(ConditionAttribute), new ConditionLogicHandler()},
                { typeof(DependencyInjectionAttribute), new DependencyInjectionAttributeLogicHandler() },
                { typeof(ExecuteUntilTimeoutAttribute), new ExecuteUntilTimeoutAttributeLogicHandler() },
                //{typeof(ExtendedTestAttribute), new ExtendedTestAttributeLogicHandler } //Is this even needed?
                { typeof(FuzzingAttribute), new FuzzingAttributeLogicHandler() },
                { typeof(GroupAttribute), new GroupAttributeLogicHandler() },
                { typeof(InjectDataAttribute), new InjectDataAttributeLogicHandler() },
                { typeof(PermutationAttribute), new PermutationAttributeLogicHandler() },
                { typeof(RandomAttribute), new RandomAttributeLogicHandler() },
                { typeof(RepetitionsAttribute), new RepetitionsAttributeLogicHandler() },
                { typeof(RetryAttribute), new RetryAttributeLogicHandler() },
                { typeof(RunAfterAttribute), new RunAfterAttributeLogicHandler() },
                { typeof(RunBeforeAttribute), new RunBeforeAttributeLogicHandler() },
                { typeof(RunInThreadAttribute), new RunInThreadAttributeLogicHandler() },
                { typeof(SkipAttribute), new SkipAttributeLogicHandler() },
                { typeof(TimeoutAttribute), new TimeoutAttributeLogicHandler() },
                { typeof(TimeoutRetryAttribute), new TimeoutRetryAttributeLogicHandler() }
                //{typeof(TestAttribute), TestLogicHandler } //is this even needed?!

                // More mappings...
            };
        }

        public IAttributeLogicHandler GetHandlerFor(Attribute attribute)
        {
            return _mapping.TryGetValue(attribute.GetType(), out var handler) ? handler : null;
        }
    }

    //public class CommonTestAttributeLogicHandler : IAttributeLogicHandler
    //{
    //    public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
    //    {
    //        // Logic for handling CommonTestAttribute
    //    }
    //}

    public class ConditionalRetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var conditionalRetryAttribute = attribute as ConditionalRetryAttribute;
            MethodInfo conditionMethod = testInstance.GetType().GetMethod(conditionalRetryAttribute.ConditionMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic); ;
            if (conditionMethod == null)
            {
                throw new InvalidOperationException("Condition method not found.");
            }

            int attempts = 0;
            bool conditionMet = false;

            while (attempts < conditionalRetryAttribute.MaxRetry || conditionalRetryAttribute.MaxRetry == -1)
            {
                testMethod.Invoke(testInstance, null);

                conditionMet = (bool)conditionMethod.Invoke(testInstance, null);
                if (conditionMet)
                    break;

                attempts++;
            }

            if (!conditionMet)
            {
                // Handle the test as failed after all retries
            }
        }
    }

    public class DependencyInjectionAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
        }
    }
    public class ConditionLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            ConditionAttribute conditionAttribute = attribute as ConditionAttribute;
            if (conditionAttribute.Condition)
            {

            }
        }
    }
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

    public class TestDataGenerator
    {
        private Random _random = new Random();

        public IEnumerable<object[]> GenerateTestData(Type parameterType)
        {
            List<object[]> testData = new List<object[]>();

            // Example for int type
            if (parameterType == typeof(int))
            {
                testData.Add(new object[] { _random.Next() }); // Random int
                testData.Add(new object[] { int.MaxValue });   // Max value
                testData.Add(new object[] { int.MinValue });   // Min value
            }

            // Example for string type
            else if (parameterType == typeof(string))
            {
                testData.Add(new object[] { Guid.NewGuid().ToString() });  // Random string
                testData.Add(new object[] { string.Empty });               // Empty string
                testData.Add(new object[] { null });                       // Null
            }

            // Add cases for other types as needed...

            return testData;
        }
    }

    public class FuzzingAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var testDataGenerator = new TestDataGenerator();
            var parameters = testMethod.GetParameters();

            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType.IsComparable() && parameter.ParameterType.IsEquatable())
                {
                    var testData = testDataGenerator.GenerateTestData(parameter.ParameterType);

                    foreach (var data in testData)
                    {
                        testMethod.Invoke(testInstance, data);
                        // Handle and record test results
                    }
                }
            }
        }
    }

    public class GroupAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
        }
    }

    public class InjectDataAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            InjectDataAttribute injectDataAttribute = attribute as InjectDataAttribute;
            testMethod.Invoke(testInstance, injectDataAttribute.Parameters);
        }
    }

    public class PermutationAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
        }
    }

    public class RandomAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {

            // Logic for handling CommonTestAttribute
            RandomAttribute randomAttribute = attribute as RandomAttribute;
            for (int i = 0; i < randomAttribute.ExecutionCount; i++)
            {
                testMethod.Invoke(testInstance, new object[] { randomAttribute.RandomValue });
            }
        }
    }

    public class RepetitionsAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RepetitionsAttribute repetitionsAttribute = attribute as RepetitionsAttribute;
            for (int i = 0; i < repetitionsAttribute.Count; i++)
            {
                testMethod.Invoke(testInstance, null);
            }
        }
    }

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

    public class RunBeforeAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            RunBeforeAttribute runBeforeAttribute = attribute as RunBeforeAttribute;
            if (DateTime.Now <= runBeforeAttribute.ExecuteBefore)
            {
                testMethod.Invoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException();
            }
        }
    }
    public class RunAfterAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            // Logic for handling CommonTestAttribute
            RunAfterAttribute runAfterAttribute = attribute as RunAfterAttribute;
            if (DateTime.Now >= runAfterAttribute.ExecuteAfter)
            {
                testMethod.Invoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException();
            }
        }
    }

    public class RunDuringAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            // Logic for handling CommonTestAttribute
            RunDuringAttribute runDuringAttribute = attribute as RunDuringAttribute;
            DateTime now = DateTime.Now;

            // Check if the current time is within the begin and end range
            if (now >= runDuringAttribute.Begin && now <= runDuringAttribute.End)
            {
                testMethod.Invoke(testInstance, null);
            }
            else
            {
                throw new ExecutionEngineException();
            }
        }
    }

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
                throw new ExecutionEngineException();
            }
        }
    }

    public class RunInThreadAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            Thread thread = new Thread(() => { testMethod.Invoke(testInstance, null); });
            thread.Start();
            thread.Join();
            // Logic for handling CommonTestAttribute
        }
    }

    public class SkipAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            // Logic for handling CommonTestAttribute
            //This is easy. Just do nothing in terms of executing. :-)
        }
    }
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

    public class TimeoutRetryAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
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
                            testMethod.Invoke(testInstance, null);
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
