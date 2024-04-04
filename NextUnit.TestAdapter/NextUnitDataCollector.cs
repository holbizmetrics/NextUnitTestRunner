using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Xml;

namespace NextUnit.TestAdapter
{
    [DataCollectorFriendlyName(Definitions.DataCollectorFriendlyName)]
    [DataCollectorTypeUri(Definitions.DataCollectorTypeUri)]
    public class NextUnitDataCollector : DataCollector
    {
        public override void Initialize(XmlElement? configurationElement, DataCollectionEvents events, DataCollectionSink dataSink, DataCollectionLogger logger, DataCollectionEnvironmentContext? environmentContext)
        {
            throw new NotImplementedException();
        }
    }
}
