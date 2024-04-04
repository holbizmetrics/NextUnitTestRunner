// See https://aka.ms/new-console-template for more information

using NextUnit.TestMethodCompletionDetector.NewFolder1;
using NextUnit.TestMethodCompleteness.NewFolder1;
using Microsoft.CodeAnalysis.CSharp;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis;

// Assume additional using directives for Roslyn namespaces

class Program
{
    private static string pathToMySolution = @"C:\Temp\NextUnitTestRunner\NextUnit.sln";

    static async Task Main(string[] args)
    {
        if (args.Length == 2 && !string.IsNullOrEmpty(args[0]) && !string.IsNullOrEmpty(args[1]))
        {
            string sourceCode = args[0];
            string testCode = args[1];
            TestCoverageAnalyzer2 testCoverageAnalyzer2 = new TestCoverageAnalyzer2();
            TestCoverageResult testCoverageResult = testCoverageAnalyzer2.Analyze(sourceCode, testCode);
            Console.WriteLine(testCoverageResult.ToString());

            testCoverageAnalyzer2.ShowDiagnostics();
           
        }
        else if (args.Length == 1 && !string.IsNullOrEmpty(args[0]))
        {
            //var settings = new TestDetectionSettings
            //{
            //    TestProjectSuffixes = new List<string> { ".Tests", ".UnitTest", ".UnitTests" },
            //    TestClassSuffixes = new List<string> { "Tests", "UnitTest", "UnitTests" },
            //    Mode = ReportMode.All
            //};

            string solutionPath = args.Length > 0 ? args[0] : pathToMySolution;

            TestDetectionSettings settings = TestDetectionSettings.NextUnit;

            if (!File.Exists(solutionPath))
            {
                Console.WriteLine($"File {solutionPath} does not exist.");
                return;
            }

            Console.WriteLine($"Analyzing solution {pathToMySolution}...");

            var solutionAnalyzer = new SolutionAnalyzer(solutionPath, settings);
            var methodTestMap = await solutionAnalyzer.AnalyzeSolutionAsync();

            var reportGenerator = new ReportGenerator(settings);
            string reportContent = reportGenerator.GenerateReportContent(methodTestMap);

            Console.WriteLine(reportContent);
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("No valid arguments found. Please specify either one argument as a solution path or two as source code and test code file. Thanks.");
        }
    }
}
