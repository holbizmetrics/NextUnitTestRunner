using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NextUnit.TestAdapter;
using NextUnit.TestExplorer;
using NextUnitTestAdapter;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NextUnit.TestExplorerUI
{
    public partial class MainViewModel : ObservableObject
    {
        private DiscoverySink discoverySink = new DiscoverySink();
        private FrameworkHandle frameworkHandle = new FrameworkHandle();

        [ObservableProperty]
        public string testExecutionText = string.Empty;

        public MainViewModel()
        {
            frameworkHandle.TestCaseRecordStarting += FrameworkHandle_TestCaseRecordStarting;
            frameworkHandle.TestCaseRecordEnding += FrameworkHandle_TestCaseRecordEnding;
            frameworkHandle.TestResultRecording += FrameworkHandle_TestResultRecording;
            frameworkHandle.MessageHandler += FrameworkHandle_MessageHandler;
        }

        private void FrameworkHandle_MessageHandler(object sender, DataCollectionMessageEventArgs e)
        {
            AddText($"{e.Level}: {e.Message}");
            AddText();
        }

        private void FrameworkHandle_TestCaseRecordStarting(object sender, RecordStartEventArgs e)
        {
            AddText($"Starting Test: {e.TestCase.DisplayName}");
        }

        private void FrameworkHandle_TestCaseRecordEnding(object sender, RecordEndEventArgs e)
        {
            string displayName = e.TestCase.DisplayName;
            string nameSpacename = GetNamespace(displayName);
            string className = GetClassName(displayName);
            string methodName = GetMethodName(displayName);
            var testMethod = Namespaces.Where(ns => ns.Name == nameSpacename)
                .SelectMany(ns => ns.Classes)
                .Where(cls => cls.Name == className)
                .SelectMany(cls => cls.Methods)
                .FirstOrDefault(mth => mth.Name == methodName);

            testMethod.Outcome = e.TestOutcome;
            AddText($"Ending Test: {e.TestCase.DisplayName}");
            AddText();
        }

        private string GetMethodName(string fullyQualifiedName)
        {
            return fullyQualifiedName;
        }

        private void FrameworkHandle_TestResultRecording(object sender, RecordResultEventArgs e)
        {
            AddText($"Result: {e.TestResult.Outcome}");
        }

        private void AddText()
        {
            AddText(Environment.NewLine);
        }

        private void AddText(string text, bool includeReturn = true)
        {
            this.TestExecutionText += text + Environment.NewLine;
        }

        [RelayCommand]
        public void DiscoverTests()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() != true) return;
            //IEnumerable<string> sources = openFileDialog.FileNames;
            IEnumerable<string> sources = new string[] { @"C:\Users\MOH1002\source\repos\NextUnit\Tests\FrameworkTests\NextUnit.Core.Tests\bin\Debug\net8.0\NextUnit.Core.Tests.dll" };
            IDiscoveryContext discoveryContext = new DiscoveryContext();
            IMessageLogger logger = new MessageLogger();
            NextUnitTestDiscoverer nextUnitTestDiscoverer = new NextUnitTestDiscoverer();
            nextUnitTestDiscoverer.DiscoverTests(sources, discoveryContext, logger, discoverySink);

            HierarchicalSorter hierarchicalSorter = new HierarchicalSorter(Namespaces);
            foreach (var testCase in discoverySink.testCases)
            {
                var nameSpaceName = GetNamespace(testCase.FullyQualifiedName);
                var className = GetClassName(testCase.FullyQualifiedName);
                var methodName = testCase.DisplayName;
                hierarchicalSorter.AddTestCaseToHierarchie(nameSpaceName, className, methodName, testCase);
            }
        }

        [ObservableProperty]
        ObservableCollection<TestNamespace> namespaces = new ObservableCollection<TestNamespace>();

        public string GetNamespace(string fullyQualifiedName)
        {
            var lastDotIndex = fullyQualifiedName.LastIndexOf('.');
            if (lastDotIndex == -1)
            {
                // no namespace found
                return string.Empty;
            }

            var lastDotIndexForClass = fullyQualifiedName.LastIndexOf(".", lastDotIndex - 1);
            if (lastDotIndexForClass == -1)
            {
                return fullyQualifiedName.Substring(0, lastDotIndex); // only namespace, no class.
            }

            return fullyQualifiedName.Substring(0, lastDotIndexForClass);
        }
        public string GetClassName(string fullyQualifiedName)
        {
            var lastDotIndex = fullyQualifiedName.LastIndexOf('.');
            if (lastDotIndex == -1)
            {
                return fullyQualifiedName; // no namespace, just the class name.
            }

            var lastDotIndexForClass = fullyQualifiedName.LastIndexOf('.', lastDotIndex - 1);
            if (lastDotIndexForClass == -1)
            {
                return fullyQualifiedName.Substring(0, lastDotIndex);
            }

            return fullyQualifiedName.Substring(lastDotIndexForClass + 1, lastDotIndex - lastDotIndexForClass - 1);
        }

        [RelayCommand]
        public void RunTests()
        {
            Thread thread = new Thread(() =>
            {
                // This should probably be the assemblies to load.
                NextUnitTestExecutor2 nextUnitTestExecutor = new NextUnitTestExecutor2();

                RunContext runContext = new RunContext();

                nextUnitTestExecutor.RunTests(discoverySink.testCases, runContext, frameworkHandle);
            });
            thread.Start();
        }
    }
}
