using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace NextUnit.TestExplorer
{
	public class DiscoveryContext : IDiscoveryContext
    {
        public IRunSettings? RunSettings => new RunSettings();
    }
}
