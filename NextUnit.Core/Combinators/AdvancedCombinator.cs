using NextUnit.Core.AttributeLogic;
using NextUnit.Core.TestAttributes.AutoFixture.NextUnit;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.Combinators
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvancedCombinator : Combinator
    {
        public override Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate) testDefinition, object instanceObject = null)
        {
            if (!testDefinition.methodInfo.HasSpecificCustomAttributes(typeof(CombineAttribute)))
            {

            }
            else
            {
                var combinator = new AttributeCombinator(testDefinition.attributes.ToArray());
            }
            return Task.FromResult(TestResult.Empty);
        }
    }
}
