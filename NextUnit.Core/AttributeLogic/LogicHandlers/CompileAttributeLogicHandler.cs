using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// Use this to compile source code during runtime and use it in your test method.
    /// </summary>
    public class CompileAttributeLogicHandler : IAttributeLogicHandler
    {
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            CompileAttribute compileAttribute = attribute as CompileAttribute;
            if (compileAttribute == null) return;

            // Compilation logic here...

            // Assume `compiledAssembly` is the result of the compilation
            var compiledAssembly = CompileSource(compileAttribute); // Simplified
            var compiledType = compiledAssembly?.GetTypes().First(); // Assuming single type for simplicity
            if (compiledType == null) return;

            object compiledObject = null;
            
            if (compiledType.IsStaticClass())
            {
                compiledObject = compiledType;
            }
            else
            {
                compiledObject = Activator.CreateInstance(compiledType, null);
            }

            // Store the compiled object in a shared context accessible by the test method
            CompiledObjectRegistry.Register(testMethod.Name, !compiledType.IsStaticClass() ? compiledObject : null);

            testMethod.Invoke(testInstance, null);
        }

        /// <summary>
        /// Holds the methods to be able to used in a "Registry".
        /// </summary>
        public static class CompiledObjectRegistry
        {
            private static readonly Dictionary<string, object> _compiledObjects = new Dictionary<string, object>();

            public static void Register(string key, object compiledObject)
            {
                _compiledObjects[key] = compiledObject;
            }

            public static object Retrieve(string key)
            {
                return _compiledObjects.TryGetValue(key, out var compiledObject) ? compiledObject : null;
            }
        }

        public static Assembly CompileSource(CompileAttribute compileAttribute)
        {
            string sourceCode = compileAttribute.UseFile ? File.ReadAllText(compileAttribute.Source) : compileAttribute.Source;

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            // Add necessary references for compilation
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ValidationAttribute).Assembly.Location), // Example for adding System.ComponentModel.DataAnnotations
                // Add other necessary assemblies here
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: Path.GetRandomFileName(),
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    // Optionally handle and log the compilation failures
                    // For example, you could log the diagnostics or throw an exception
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        Console.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
                    }
                    return null;
                }

                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
        }

        // CompiledObjectRegistry implementation remains the same
    }
}
