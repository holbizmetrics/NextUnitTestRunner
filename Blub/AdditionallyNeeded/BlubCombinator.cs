using NextUnit.Core;
using NextUnit.Core.Combinators;
using System.Reflection;

namespace Blub.AdditionallyNeeded
{
    public class BlubCombinator : Combinator
    {
        public override Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition, object classInstance = null)
        {
            throw new NotImplementedException();
        }
    }
}
