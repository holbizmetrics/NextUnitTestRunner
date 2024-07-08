using NextUnit.Core;
using NextUnit.Core.Combinators;
using System.Reflection;

namespace Blub.AdditionallyNeeded
{
    public class ExampleCombinator : Combinator
    {
        private DefaultCombinator combinator = new DefaultCombinator();
        public override Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition, object classInstance = null)
        {
            return combinator.ProcessCombinedAttributes(testDefinition, classInstance);
        }
    }
}
