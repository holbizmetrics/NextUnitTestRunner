using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.TestRunner
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
        public override async void ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null)
        {
            Type type = testDefinition.type;
            MethodInfo methodInfo = testDefinition.methodInfo;
            IEnumerable<Attribute> attributes = testDefinition.attributes;

            Type[] unallowedTypes = new Type[] { typeof(TestAttribute), typeof(GroupAttribute), typeof(SkipAttribute) };

            // Definitely the check for async would - for the current design - have to move into the single Attribute Logic Mappers.
            foreach (Attribute attribute in attributes)
            {
                if (!unallowedTypes.Contains(attribute.GetType()) && attribute.GetType().Namespace.Contains("NextUnit."))
                {
                    var handler = AttributeLogicMapper.GetHandlerFor(attribute);
                    handler?.ProcessAttribute(attribute, methodInfo, classInstance);
                    if (handler != null)
                    {
                        //testResult.State = ExecutionState.Passed;
                    }
                }
            }

            if ((attributes.Count() == 1 && attributes.First() is TestAttribute) || methodInfo.HasAsyncMethodAttributes())
            {
                if (methodInfo.IsAsyncMethod())
                {
                    var task = (Task)methodInfo.Invoke(classInstance, null); // Assuming no parameters for simplicity
                    await task.ConfigureAwait(false);
                    //testResult.State = ExecutionState.Passed;
                    // Handle the result of the async test execution
                }
                else
                {
                    methodInfo.Invoke(classInstance, null);
                    //testResult.State = ExecutionState.Passed;
                }
            }
        }
    }
}