using NextUnit.Core.AttributeLogic;
using System.Reflection;

namespace NextUnit.Core.Combinators
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvancedCombinator : Combinator
    {
        public override Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object instanceObject = null)
        {
            var combinator = new AttributeCombinator(testDefinition.attributes.ToArray());
            return Task.FromResult(TestResult.Empty);
        }
    }
}
