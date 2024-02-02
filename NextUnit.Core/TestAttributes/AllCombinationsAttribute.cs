namespace NextUnit.Core.TestAttributes
{
    public enum PermutationStrategy
    {
        AllCombinations,
        Pairwise,
        OrthogonalArray
    }

    /// <summary>
    /// Combined attribute with conditions and permutation strategies
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllCombinationsAttribute : CommonTestAttribute
    {
        public string ConditionMethodName { get; set; }
        public PermutationStrategy Strategy { get; set; }

        public AllCombinationsAttribute(
            string conditionMethodName = null,
            PermutationStrategy strategy = PermutationStrategy.AllCombinations)
        {
            ConditionMethodName = conditionMethodName;
            Strategy = strategy;
        }
    }
}
