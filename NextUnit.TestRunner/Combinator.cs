using System.Reflection;

namespace NextUnit.TestRunner
{
    public abstract class Combinator : ICombinator
    {
        public abstract void ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null);
    }

    public interface ICombinator
    {
        void ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null);
    }
}