using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

namespace NextUnit.TestMethodCompletionDetector.NewFolder1
{
    public class TestCoverageAnalyzer
    {
        public void Analyze(string classCode, string testCode)
        {
            var classTree = CSharpSyntaxTree.ParseText(classCode);
            var testTree = CSharpSyntaxTree.ParseText(testCode);
            var compilation = CSharpCompilation.Create("Analysis")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(classTree, testTree);

            // Get semantic model for classTree
            var classModel = compilation.GetSemanticModel(classTree);
            var testModel = compilation.GetSemanticModel(testTree);

            var publicMethodSymbols = classTree.GetRoot().DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(method => classModel.GetDeclaredSymbol(method))
                .Where(symbol => symbol != null && symbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public)
                .ToList();

            var testMethodInvocations = testTree.GetRoot().DescendantNodes()
                .OfType<InvocationExpressionSyntax>();

            foreach (var methodSymbol in publicMethodSymbols)
            {
                var isTested = testMethodInvocations.Any(invocation =>
                {
                    var invokedSymbol = testModel.GetSymbolInfo(invocation).Symbol;
                    return invokedSymbol?.Equals(methodSymbol) ?? false;
                });

                if (!isTested)
                {
                    Console.WriteLine($"Method {methodSymbol.Name} does not appear to be tested.");
                }
            }
        }

        // Overloaded Analyze method to take a Type and testCode as string
        public void Analyze(Type type, string testCode)
        {
            // Use reflection to get PEMs from the Type
            var methodNames = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
.Select(m => m.Name).ToList();

            var propertyNames = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
.Select(p => p.Name).ToList();

            var eventNames = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
.Select(e => e.Name).ToList();

            // Combine PEM names
            var allMemberNames = methodNames.Concat(propertyNames).Concat(eventNames).ToList();

            // Analyze the test code using Roslyn
            var testTree = CSharpSyntaxTree.ParseText(testCode);
            var testRoot = testTree.GetRoot();
            var testMethodInvocations = testRoot.DescendantNodes()
                .OfType<InvocationExpressionSyntax>();

            // Logic to analyze if PEMs are tested goes here
            // This will involve parsing the test code and looking for references to the PEMs
            // Example structure for methods; similar approach for properties and events
            foreach (var methodName in methodNames)
            {
                var isTested = testMethodInvocations.Any(invocation =>
                {
                    // Simplified: just checking if the method name appears in the test code
                    // In a real scenario, you'd need more sophisticated analysis
                    // to match method calls accurately
                    return invocation.ToString().Contains(methodName);
                });

                if (!isTested)
                {
                    Console.WriteLine($"Method {methodName} does not appear to be tested.");
                }
            }

            // Extend this logic to check for property and event coverage as needed
        }

        public Type LoadTypeFromAssembly(string AssemblyPath, Type type)
        {
            return LoadTypeFromAssembly(AssemblyPath, type.FullName);
        }

        // New method to load a type from an assembly
        public Type LoadTypeFromAssembly(string assemblyPath, string typeName)
        {
            try
            {
                // Load the assembly from the specified path
                var assembly = Assembly.LoadFrom(assemblyPath);
                // Try to get the type by its fully qualified name
                var type = assembly.GetType(typeName, throwOnError: true);
                return type;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading type from assembly: {ex.Message}");
                return null;
            }
        }

        public object CreateInstanceOfType(string assemblyPath, string typeName)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var type = assembly.GetType(typeName);
            if (type != null)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public void ReportTestCoverageProgress(TestCoverageResult result)
        {
            if (result.TotalMethodsCount == 0)
            {
                Console.WriteLine("No methods found for analysis.");
            }
            else
            {
                Console.WriteLine($"{result.TestedMethods} of {result.TotalMethodsCount} methods are tested. Tested: {result.TestedPercentage:0.00}%, Untested: {result.UntestedPercentage:0.00}%");
                if (result.UntestedMethods.Any())
                {
                    Console.WriteLine("Untested methods:");
                    foreach (var method in result.UntestedMethods)
                    {
                        Console.WriteLine($"- {method}");
                    }
                }
            }
        }
    }
}
