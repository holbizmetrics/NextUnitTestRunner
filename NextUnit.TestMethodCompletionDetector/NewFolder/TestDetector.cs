using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NextUnit.TestMethodCompleteness.NewFolder1
{
    public class TestDetector
    {
        private readonly TestDetectionSettings _settings;
        private readonly HashSet<string> nUnitTestAttributes = new HashSet<string>
        {
            "Test", "TestCase", "NUnit.Framework.Test", "NUnit.Framework.TestCase"
        };

        public TestDetector(TestDetectionSettings settings)
        {
            _settings = settings;
        }

        public async Task DetectTests(SyntaxNode syntaxRoot, Dictionary<string, List<string>> methodTestMap, Project testProject)
        {
            var classes = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();
            foreach (var classDeclaration in classes)
            {
                var className = classDeclaration.Identifier.ValueText;
                // Iterate over each possible test class suffix
                foreach (var suffix in _settings.TestClassSuffixes)
                {
                    var testClassName = $"{className}{suffix}";

                    foreach (var testDocument in testProject.Documents)
                    {
                        var testSyntaxRoot = await testDocument.GetSyntaxRootAsync();
                        var testClasses = testSyntaxRoot.DescendantNodes()
                                                         .OfType<ClassDeclarationSyntax>()
                                                         .Where(c => c.Identifier.ValueText.Equals(testClassName, StringComparison.Ordinal));


                        foreach (var testClass in testClasses)
                        {
                            var sourceMethods = classDeclaration.Members
                                                                 .OfType<MethodDeclarationSyntax>()
                                                                 .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword)) // Only interested in public methods
                                                                 .Select(m => m.Identifier.ValueText);

                            var testMethods = testClass.Members
                                                        .OfType<MethodDeclarationSyntax>()
                                                        .Select(m => m.Identifier.ValueText);

                            foreach (var sourceMethod in sourceMethods)
                            {
                                var expectedTestMethodName = $"{sourceMethod}Test"; // Naming convention
                                if (!testMethods.Any(t => t.Equals(expectedTestMethodName, StringComparison.OrdinalIgnoreCase)))
                                {
                                    var key = $"{className}.{sourceMethod}";
                                    if (!methodTestMap.ContainsKey(key))
                                    {
                                        methodTestMap[key] = new List<string>(); // Indicate missing test
                                    }
                                    // Optionally, add specific info about the missing test
                                    // methodTestMap[key].Add($"Missing test for {key}");
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsUnitTest(MethodDeclarationSyntax method)
        {
            return method.AttributeLists.SelectMany(a => a.Attributes).Any(a =>
                nUnitTestAttributes.Contains(a.Name.ToString()) ||
                nUnitTestAttributes.Any(attr => a.Name.ToString().EndsWith(attr)));
        }
    }
}