namespace NextUnit.TestMethodCompleteness.NewFolder1
{
    public class TestMethodMapper
    {
        private readonly Dictionary<string, List<string>> _methodTestMap = new Dictionary<string, List<string>>();

        public void MapTestMethod(string testMethodName, string className)
        {
            // Assuming a simple naming convention: <MethodName>Test
            var sourceMethodName = testMethodName.EndsWith("Test")
                ? testMethodName.Substring(0, testMethodName.Length - "Test".Length)
                : testMethodName;

            // Fully qualified source method name
            var fullyQualifiedSourceName = $"{className}.{sourceMethodName}";

            if (!_methodTestMap.ContainsKey(fullyQualifiedSourceName))
            {
                _methodTestMap[fullyQualifiedSourceName] = new List<string>();
            }
            _methodTestMap[fullyQualifiedSourceName].Add(testMethodName);
        }

        public Dictionary<string, List<string>> GetMethodTestMap() => _methodTestMap;

        // Additional logic here to handle ambiguities or refine the mapping strategy
    }
}
