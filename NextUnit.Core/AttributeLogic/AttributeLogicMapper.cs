using NextUnit.Core.AttributeLogic.LogicHandlers;
using NextUnit.Core.TestAttributes;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic
{
    public class AttributeLogicMapper
    {
        protected readonly Dictionary<Type, IAttributeLogicHandler> _mapping;

        public AttributeLogicMapper()
        {
            _mapping = new Dictionary<Type, IAttributeLogicHandler>
            {
                { typeof(AllCombinationsAttribute), new AllCombinationsAttributeLogicHandler() },
                //{ typeof(CommonTestAttribute), new CommonTestAttributeLogicHandler() }, //Is this even needed?
                { typeof(DebugAttribute), new DebugAttributeLogicHandler() },
                { typeof(DebuggerBreakAttribute), new DebugAttributeLogicHandler() },
                { typeof(CompileAttribute), new CompileAttributeLogicHandler() },
                { typeof(CustomExtendableAttribute), new CustomExtendableAttributeLogicHandler() },
                { typeof(ConditionalRetryAttribute), new ConditionalRetryAttributeLogicHandler() },
                { typeof(ConditionAttribute), new ConditionAttributeLogicHandler()},
                { typeof(DependencyInjectionAttribute), new DependencyInjectionAttributeLogicHandler() },
                { typeof(ExecuteUntilTimeoutAttribute), new ExecuteUntilTimeoutAttributeLogicHandler() },
                //{typeof(ExtendedTestAttribute), new ExtendedTestAttributeLogicHandler } //Is this even needed?
                { typeof(FuzzingAttribute), new FuzzingAttributeLogicHandler() },
                { typeof(GroupAttribute), new GroupAttributeLogicHandler() },
                { typeof(InjectDataAttribute), new InjectDataAttributeLogicHandler() },
                { typeof(PermutationAttribute), new PermutationAttributeLogicHandler() },
                { typeof(RunAllDelegatePermutationsAttribute), new RunAllDelegatePermutationsLogicHandler() },
                { typeof(RandomAttribute), new RandomAttributeLogicHandler() },
                { typeof(RepetitionsAttribute), new RepetitionsAttributeLogicHandler() },
                { typeof(RetryAttribute), new RetryAttributeLogicHandler() },
                { typeof(RunAfterAttribute), new RunAfterAttributeLogicHandler() },
                { typeof(RunBeforeAttribute), new RunBeforeAttributeLogicHandler() },
                {typeof(RunIfEnvVarAttribute), new RunIfEnvVarAttributeLogicHandler() },
                { typeof(RunInThreadAttribute), new RunInThreadAttributeLogicHandler() },
                { typeof(SkipAttribute), new SkipAttributeLogicHandler() },
                { typeof(TimeoutAttribute), new TimeoutAttributeLogicHandler() },
                { typeof(TimeoutRetryAttribute), new TimeoutRetryAttributeLogicHandler() }
                //{typeof(TestAttribute), TestLogicHandler } //is this even needed?!

                // More mappings...
            };
        }

        public virtual IAttributeLogicHandler GetHandlerFor(Attribute attribute)
        {
            Type attributeType = attribute.GetType();
            IAttributeLogicHandler attributeLogicHandler = _mapping.TryGetValue(attributeType, out var handler) ? handler : null;
            if (attributeLogicHandler == null)
            {
                if (attribute.GetType().BaseType == typeof(CustomExtendableAttribute))
                {
                    attributeLogicHandler = _mapping[typeof(CustomExtendableAttribute)];
                }
            }
            return attributeLogicHandler;
        }

        public IAttributeLogicHandler GetHandlersFor((Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition, object classObject)
        {
            IEnumerable<Attribute> attributes = definition.Attributes;
            IAttributeLogicHandler attributeLogicHandler = null;
            if (attributes != null && attributes.Count() == 1 && attributes.First() is CommonTestAttribute)
            {
                attributeLogicHandler = _mapping.TryGetValue(typeof(CommonTestAttribute), out var handler) ? handler : null;
            }
            else
            {
                if (attributes.Any(x=>x is SkipAttribute))
                {
                    attributeLogicHandler = _mapping.TryGetValue(typeof(SkipAttribute), out var handler) ? handler: null;
                    return attributeLogicHandler;
                }
                foreach (Attribute attribute in attributes)
                {
                    Type attributeType = attribute.GetType();
                    attributeLogicHandler = _mapping.TryGetValue(attributeType, out var handler) ? handler : null;
                    handler?.ProcessAttribute(attribute, definition.methodInfo, classObject);
                }
            }
            return null;
        }
    }

    //public class CommonTestAttributeLogicHandler : IAttributeLogicHandler
    //{
    //    public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
    //    {
    //        // Logic for handling CommonTestAttribute
    //    }
    //}
}
