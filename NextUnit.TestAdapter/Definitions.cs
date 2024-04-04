using System.Reflection;

namespace NextUnit.TestAdapter
{
    public static class Definitions
    {
        public const string ExecutorURI = "executor://NextUnitTestDiscoverer";
        public const string DiscovererURI = "executor://NextUnitTestDiscoverer";
        public const string DataCollectorFriendlyName = "NewDataCollector";
        public const string DataCollectorTypeUri = "my://new/datacollector";
        public const string dll = ".dll";
        public const string exe = ".exe";
        public const string managed = "managed";
        public const string unmanaged = "unmanaged";
        public static BindingFlags GeneralBindingFlags { get; } = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
    }
}
