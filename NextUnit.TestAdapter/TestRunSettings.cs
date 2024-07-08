using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System.Xml;

namespace NextUnit.TestAdapter
{
	public class TestAdapterTestRunSettings : TestRunSettings, IRunSettings
	{
		public TestAdapterTestRunSettings(string name) : base(name)
		{
		}

		public string? SettingsXml => throw new NotImplementedException();

		public ISettingsProvider? GetSettings(string? settingsName)
		{
			throw new NotImplementedException();
		}

		public override XmlElement ToXml()
		{
			throw new NotImplementedException();
		}
	}
}
