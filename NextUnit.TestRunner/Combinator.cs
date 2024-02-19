using System.Reflection;

namespace NextUnit.TestRunner
{
    public abstract class Combinator : ICombinator
    {
        public TestResult CurrentTestResult { get; set; } = null;

        public abstract Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null);
    }

    public interface ICombinator
    {
        Task<TestResult> ProcessCombinedAttributes((Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes) testDefinition, object classInstance = null);
        public TestResult CurrentTestResult { get; set; }
    }
}