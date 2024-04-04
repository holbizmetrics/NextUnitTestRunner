using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace NextUnit.TestExplorer
{
    public class RunSettings : IRunSettings
    {
        public string? SettingsXml => throw new NotImplementedException();

        public ISettingsProvider? GetSettings(string? settingsName)
        {
            throw new NotImplementedException();
        }
    }
}
