using System.Collections.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class TestNamespace
    {
        public string Name { get; set; }
        public ObservableCollection<TestClass> Classes { get; set; } = new ObservableCollection<TestClass>();
    }
}
