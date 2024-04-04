using System.Collections.ObjectModel;

namespace NextUnit.TestExplorerUI
{
    public class TestClass
    {
        public string Name { get; set; }
        public ObservableCollection<TestMethod> Methods { get; set; } = new ObservableCollection<TestMethod>();
    }
}