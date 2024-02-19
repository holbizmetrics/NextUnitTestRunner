using System.Reflection;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvancedCombinator : Combinator
    {
        public override Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object instanceObject = null)
        {
            //var combinator = new AttributeCombinator(customAttributes);
            return Task.FromResult(TestResult.Empty);
        }
    }
}
