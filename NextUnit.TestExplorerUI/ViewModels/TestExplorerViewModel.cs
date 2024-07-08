using CommunityToolkit.Mvvm.ComponentModel;
using NextUnit.TestExplorer;
using System.Collections.ObjectModel;

namespace NextUnit.TestExplorerUI.ViewModels
{
    public class TestExplorerViewModel : ObservableObject
    {
        public ObservableCollection<TestNamespace> Namespaces { get; set; } = new ObservableCollection<TestNamespace>();
    }
}