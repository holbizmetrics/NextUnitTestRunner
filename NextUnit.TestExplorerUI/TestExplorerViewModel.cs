using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace NextUnit.TestExplorerUI
{
    public class TestExplorerViewModel : ObservableObject
    {
        public ObservableCollection<TestNamespace> Namespaces { get; set; } = new ObservableCollection<TestNamespace>();

    }
}
