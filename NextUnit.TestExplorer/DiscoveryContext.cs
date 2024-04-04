using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestExplorer
{
    public class DiscoveryContext : IDiscoveryContext
    {
        public IRunSettings? RunSettings => throw new NotImplementedException();
    }
}
