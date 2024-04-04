using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Collections.ObjectModel;

namespace NextUnit.TestExplorerUI
{
    public class HierarchicalSorter
    {
        public ObservableCollection<TestNamespace> Namespaces { get; set; } = null;
        public HierarchicalSorter(ObservableCollection<TestNamespace> namespaces)
        {
            Namespaces = namespaces;
        }
        public void AddTestCaseToHierarchie(string namespaceName, string className, string methodName, TestCase testCase)
        {
            var nameSpaceNode = Namespaces.FirstOrDefault(nameof => nameof.Name == namespaceName);
            if (nameSpaceNode == null)
            {
                nameSpaceNode = new TestNamespace { Name = namespaceName };
                Namespaces.Add(nameSpaceNode);
            }

            var classNode = nameSpaceNode.Classes.FirstOrDefault(c => c.Name == className);
            if (classNode == null)
            {
                classNode = new TestClass { Name = className };
                nameSpaceNode.Classes.Add(classNode);
            }

            var methodNode = new TestMethod { Name = methodName, TestCase = testCase };
            classNode.Methods.Add(methodNode);
        }
    }
}
