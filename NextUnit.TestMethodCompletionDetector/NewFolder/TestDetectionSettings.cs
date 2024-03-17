namespace NextUnit.TestMethodCompleteness.NewFolder1
{
    public class TestDetectionSettings : ITestDetectionSettings
    {
        public List<string> TestProjectSuffixes { get; set; } = new List<string> { ".Tests", ".UnitTest", ".UnitTests" };
        public List<string> TestClassSuffixes { get; set; } = new List<string> { "Tests", "UnitTest", "UnitTests" };
        // You can add more settings here as needed
        public ReportMode Mode { get; set; } = ReportMode.All; // Default to showing all

        public static TestDetectionSettings MSTest => new TestDetectionSettings
        {
            TestProjectSuffixes = new List<string> { ".Tests" },
            TestClassSuffixes = new List<string> { "Test" }
        };

        public static TestDetectionSettings NUnit => new TestDetectionSettings
        {
            TestProjectSuffixes = new List<string> { ".Tests", ".UnitTests" },
            TestClassSuffixes = new List<string> { "Tests", "UnitTests" }
        };

        public static TestDetectionSettings XUnit => new TestDetectionSettings
        {
            TestProjectSuffixes = new List<string> { ".Tests", ".UnitTests" },
            TestClassSuffixes = new List<string> { "Tests", "UnitTests" }
        };

        public static TestDetectionSettings NextUnit => new TestDetectionSettings
        {
            TestProjectSuffixes = new List<string> { ".NextUnitTests", ".NextUnit.UnitTests" },
            TestClassSuffixes = new List<string> { "NextUnitTests", "NextUnitTest" }
        };
    }

    public interface ITestDetectionSettings
    {
        //public ReportMode ReportMode { get; set; }
    }

    public enum ReportMode
    {
        All,        // Show all methods, indicating which have tests and which do not
        WithTests,  // Only show methods that have corresponding tests
        WithoutTests // Only show methods that do not have corresponding tests
    }
}
