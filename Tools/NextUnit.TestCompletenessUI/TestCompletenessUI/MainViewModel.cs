using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NextUnit.TestMethodCompletionDetector.NewFolder1;
using System.IO;

namespace NextUnit.TestMethodCompletionDetector
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public string sourceFilePath = string.Empty;

        [ObservableProperty]
        public string testFilePath = string.Empty;

        [ObservableProperty]
        public string analysisResult = string.Empty;

        public MainViewModel()
        {

        }

        private void OnDrop(string[] files)
        {
            // handle dropped files here (e.g. update textboxes as needed)
        }

        [RelayCommand]
        void ChooseSourceFile()
        {
            string readFile = GetFileName();
            if (string.IsNullOrEmpty(readFile))
            {
                return;
            }
            SourceFilePath = readFile;
        }

        [RelayCommand]
        void ChooseTestFile()
        {
            string readFile = GetFileName();
            if (string.IsNullOrEmpty(readFile))
            {
                return;
            }
            TestFilePath = readFile;
        }

        [RelayCommand]
        void Analyze()
        {
            string sourceFileCode = string.Empty;
            
            if (File.Exists(SourceFilePath))
            {
                sourceFileCode = ReadFile(SourceFilePath);
            }

            string testFileCode = string.Empty;
            if (File.Exists(TestFilePath))
            {
                testFileCode = ReadFile(TestFilePath);
            }

            if (string.IsNullOrEmpty(sourceFileCode) || string.IsNullOrEmpty(testFileCode))
            {
                return;
            }

            TestCoverageAnalyzer2 testCoverageAnalyzer2 = new TestCoverageAnalyzer2();
            TestCoverageResult testCoverageResult = testCoverageAnalyzer2.Analyze(sourceFileCode, testFileCode);
            AnalysisResult = testCoverageResult.ToString();
        }

        private string ReadFile(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        private string GetFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "c# Source Files (*.cs)|*.cs|All files (*.*)|*.*";
            if ((bool)!openFileDialog.ShowDialog()) return string.Empty;
            return openFileDialog.FileName;
        }
    }
}
