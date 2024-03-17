using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace NextUnit.TestMethodCompleteness.NewFolder1
{
    public class SolutionAnalyzer
    {
        private readonly TestDetectionSettings _settings;
        public string SolutionPath { get; }

        public SolutionAnalyzer(string solutionPath, TestDetectionSettings settings)
        {
            SolutionPath = solutionPath;
            _settings = settings;
        }

        public async Task<Dictionary<string, List<string>>> AnalyzeSolutionAsync()
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(SolutionPath);
            var methodTestMap = new Dictionary<string, List<string>>();

            var sourceProjects = solution.Projects.Where(p => !_settings.TestProjectSuffixes.Any(suffix => p.Name.EndsWith(suffix)));
            var testProjects = solution.Projects.Except(sourceProjects).ToList();

            foreach (var sourceProject in sourceProjects)
            {
                var testProject = testProjects.FirstOrDefault(p =>
                                _settings.TestProjectSuffixes.Any(suffix => p.Name.Equals($"{sourceProject.Name}{suffix}", StringComparison.OrdinalIgnoreCase)));
                if (testProject != null)
                {
                    // Now for each sourceProject, find its matching testProject and analyze
                    foreach (var document in sourceProject.Documents)
                    {
                        var syntaxRoot = await document.GetSyntaxRootAsync();
                        var testDetector = new TestDetector(_settings);
                        testDetector.DetectTests(syntaxRoot, methodTestMap, testProject);
                    }
                }
            }

            return methodTestMap;
        }
    }
}
