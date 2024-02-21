using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Reflection;

namespace NextUnit.Core.Combinators
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCombinator : Combinator
    {
        /// <summary>
        /// TODO: if we use it like this this will throw all of the AutoFixture/AutoMoq attributes out of the window for now. Not good and obviously wrong.
        /// But for now it makes things at least working again.
        /// </summary>
        public AttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();
        public override async Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null)
        {
            TestResult testResult = TestResult.Empty;
            // Filter and validate attributes
            var executionAttributes = testDefinition.attributes
                .Where(attr => !(attr is GroupAttribute || attr is TestAttribute))
                .ToList();

            // Check for exactly one TestAttribute and no other attributes
            if (testDefinition.attributes.Count() == 1 && testDefinition.attributes.Any(attr => attr is TestAttribute)
                || testDefinition.attributes.Any(attr => attr is GroupAttribute))
            {
                // Add the single TestAttribute back for processing
                executionAttributes.Add(testDefinition.attributes.First(attr => attr is TestAttribute));
            }
            // Check if there's exactly one TestAttribute, regardless of the number of GroupAttribute instances
            else if (testDefinition.attributes.Count(attr => !(attr is GroupAttribute)) == 1 && testDefinition.attributes.Any(attr => attr is TestAttribute))
            {
                // Ensure the single TestAttribute is included for processing, this step might be redundant if TestAttribute was not excluded initially
                executionAttributes.Add(testDefinition.attributes.First(attr => attr is TestAttribute));
            }

            if (testDefinition.methodInfo.Name.Contains("AssertIsFalse_Successful_Test"))
            {
                StackFrame stackFrame = new StackFrame();
                List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

                var debugObject = new {
                    ExecutingAssembly = Assembly.GetExecutingAssembly(),
                    CallingAssembly = Assembly.GetCallingAssembly(),
                    HasNativeImage = stackFrame.HasNativeImage(),
                    NativeImageBase = stackFrame.GetNativeImageBase(),
                    NativeIP = stackFrame.GetNativeIP(),
                    FileColumnNumber = stackFrame.GetFileColumnNumber(),
                    FileLineNumber = stackFrame.GetFileLineNumber(),
                    ILOffset = stackFrame.HasILOffset() ? stackFrame.GetILOffset() : -1,
                    NativeOffset = stackFrame.GetNativeOffset(),
                    Files = files
                };

            }

            Type type = testDefinition.type;
            MethodInfo methodInfo = testDefinition.methodInfo;
            IEnumerable<Attribute> attributes = testDefinition.attributes;

            Stopwatch stopwatch = new Stopwatch();

            if (executionAttributes.Count() == 1 && executionAttributes.First() is TestAttribute)
            {
                PrepareTestResult(testDefinition, testResult, methodInfo.DeclaringType);
                if (methodInfo.IsAsyncMethod())
                {
                    //end the TestResult preparation.
                    stopwatch = Stopwatch.StartNew();
                    var task = (Task)methodInfo.Invoke(classInstance, null); // Assuming no parameters for simplicity
                    await task.ConfigureAwait(false);

                    EndTestResult(testResult, stopwatch);

                    // Handle the result of the async test execution
                    testResult.State = ExecutionState.Passed;
                }
                else
                {
                    methodInfo.Invoke(classInstance, null);

                    EndTestResult(testResult, stopwatch);
                }
                return testResult;
            }

            // Definitely the check for async would - for the current design - have to move into the single Attribute Logic Mappers.
            foreach (Attribute attribute in executionAttributes)
            {
                //We shouldn't have a TestAttribute here, but if so, skip it...
                if (attribute is TestAttribute)
                {
                    continue;
                }
                if (attribute.GetType().Namespace.Contains("NextUnit."))
                {
                    //Start with the TestResult preparation.
                    Type declaringType = testDefinition.methodInfo.DeclaringType;
                    PrepareTestResult(testDefinition, testResult, declaringType);

                    stopwatch = Stopwatch.StartNew();
                    testResult.Start = DateTime.Now;

                    testResult.State = ExecutionState.Running;

                    var handler = AttributeLogicMapper.GetHandlerFor(attribute);
                    handler?.ProcessAttribute(attribute, methodInfo, classInstance);

                    //end the TestResult preparation.
                    EndTestResult(testResult, stopwatch);
                }
                else if (attribute is SkipAttribute)
                {
                    testResult.State = ExecutionState.Skipped;
                }
            }
            return testResult;
        }

        private static void PrepareTestResult((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, TestResult testResult, Type declaringType)
        {
            testResult.Namespace = declaringType.ToString();
            testResult.Class = declaringType.Name;
            if (string.IsNullOrEmpty(testResult.Class))
            {

            }
            testResult.Workstation = Environment.MachineName;
            testResult.DisplayName = testDefinition.methodInfo.Name;
        }

        private static void EndTestResult(TestResult testResult, Stopwatch stopwatch)
        {
            testResult.ExecutionTime = stopwatch.Elapsed;
            stopwatch.Stop();
            testResult.End = DateTime.Now;
            testResult.State = ExecutionState.Passed;
        }
    }
}