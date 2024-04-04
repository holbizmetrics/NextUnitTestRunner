using Microsoft.CodeAnalysis;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NextUnit.Core.Combinators
{
    /// <summary>
    /// Default Combinator to execute tests.
    /// </summary>
    public class DefaultCombinator : Combinator
    {
        /// <summary>
        /// TODO: if we use it like this this will throw all of the AutoFixture/AutoMoq attributes out of the window for now. Not good and obviously wrong.
        /// But for now it makes things at least working again.
        /// </summary>
        public AttributeLogicMapper AttributeLogicMapper { get; set; } = new AttributeLogicMapper();
        public override async Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition, object classInstance = null)
        {
            TestResult testResult = TestResult.Empty;
            // Filter and validate attributes
            // Filter and validate attributes, excluding AsyncStateMachineAttribute
            var executionAttributes = testDefinition.attributes
                .Where(attr => !(attr is GroupAttribute || attr is TestAttribute || attr is NullableContextAttribute || attr.GetType().IsDefined(typeof(AsyncStateMachineAttribute), inherit: false)))
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
            else if (testDefinition.attributes.Any(attr => attr is TestAttribute) && testDefinition.attributes.Count(attr => attr is GroupAttribute) >= 0)
            {
                //TODO: this is not completely correct. The problems will definitely solved in the new combinator design.
                executionAttributes.Add(testDefinition.attributes.First(attr => attr is TestAttribute));
            }
            else if (testDefinition.attributes.Any(attr => attr is TestAttribute) && testDefinition.attributes.Any(attr => attr is AsyncStateMachineAttribute) && testDefinition.attributes.Count(attr => attr is GroupAttribute) >= 0)
            {
                executionAttributes.Clear();
                executionAttributes.Add(testDefinition.attributes.First(attr => attr is TestAttribute));
            }
            // why does this else even exists when it's empty? Because we shouldn't come here. This is still good for debugging reasons.
            else
            {
                // we shouldn't get here.
            }

            if (executionAttributes.Count() == 0)
            {
                throw new ExecutionEngineException("No execution attributes found!");
            }
            executionAttributes.Remove(new AsyncStateMachineAttribute(typeof(void)));

            StackFrame stackFrame = new StackFrame();
            List<string> files = new StackTrace().GetFrames()?.Select((StackFrame x) => x.GetMethod()?.DeclaringType?.Assembly.CodeBase).Distinct().ToList();

            // This will be just used for diagnoses for now to get rid of errors.
            var debugObject = new
            {
                ExecutingAssembly = Assembly.GetExecutingAssembly(),
                CallingAssembly = Assembly.GetCallingAssembly(),
                HasNativeImage = stackFrame.HasNativeImage(),
                NativeImageBase = stackFrame.GetNativeImageBase(),
                NativeIP = stackFrame.GetNativeIP(),
                FileColumnNumber = stackFrame.GetFileColumnNumber(),
                FileLineNumber = stackFrame.GetFileLineNumber(),
                ILOffset = stackFrame.HasILOffset() ? stackFrame.GetILOffset() : -1,
                NativeOffset = stackFrame.GetNativeOffset(),
                Files = files,
                Method = testDefinition.methodInfo,
                Delegate = testDefinition.@delegate,
                Type = testDefinition.type,
                Attributes = testDefinition.attributes,
            };

            Type type = testDefinition.type;
            MethodInfo methodInfo = testDefinition.methodInfo;
            IEnumerable<Attribute> attributes = testDefinition.attributes;

            if (executionAttributes.Count() == 1 && executionAttributes.First() is TestAttribute)
            {
                //end the TestResult preparation.
                if (methodInfo.IsAsyncMethod())
                {
                    var task = (Task)methodInfo.Invoke(classInstance, testDefinition.@delegate, null); // Assuming no parameters for simplicity
                    await task.ConfigureAwait(false);


                    // Handle the result of the async test execution
                    testResult.State = ExecutionState.Passed;
                }
                else
                {
                    //var handler = AttributeLogicMapper.GetHandlerFor(attribute);
                    //handler?.ProcessAttribute(attribute, methodInfo, classInstance);
                    methodInfo.Invoke(classInstance, testDefinition.@delegate, null);
                }
                testResult.State = ExecutionState.Passed;
                return testResult;
            }

            string[] namespaces = new string[] { "NextUnit.", "AutoFixture.NextUnit" };

            // Definitely the check for async would - for the current design - have to move into the single Attribute Logic Mappers.
            foreach (Attribute attribute in executionAttributes)
            {
                //We shouldn't have a TestAttribute here, but if so, skip it...
                if (attribute is TestAttribute || attribute is AsyncIteratorStateMachineAttribute || attribute is AsyncStateMachineAttribute || attribute is DebuggerStepThroughAttribute || attribute is DebuggerStepperBoundaryAttribute)
                {
                    continue;
                }
                string attributeNameSpace = attribute.GetType().Namespace;
                if (attributeNameSpace.Contains("NextUnit.") || attributeNameSpace.Contains("AutoFixture.NextUnit"))
                {
                    //Start with the TestResult preparation.
                    Type declaringType = testDefinition.methodInfo.DeclaringType;

                    testResult.State = ExecutionState.Running;

                    var handler = AttributeLogicMapper.GetHandlerFor(attribute);
                    handler?.ProcessAttribute(attribute, testDefinition.methodInfo, testDefinition.@delegate, classInstance);

                    //end the TestResult preparation.
                    testResult.State = ExecutionState.Passed;
                }
                else if (attribute is SkipAttribute)
                {
                    testResult.State = ExecutionState.Skipped;
                }
                else
                {
                    //What namespace are we in?
                    string nameSpace = attribute.GetType().Namespace;
                }
            }
            return testResult;
        }
    }
}