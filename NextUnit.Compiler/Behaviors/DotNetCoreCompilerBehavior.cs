using NextUnit.Compiler.CompileCore;
using NextUnit.Compiler.Extensions;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace NextUnit.Compiler.Behaviors
{
    public class DotNetCoreCompilerBehavior : ICompilerBehavior
    {
        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        private static IEnumerable<string> DefaultNamespaces = new[]
        {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
        };

        public void AddUsing(string assemblyDll)
        {
            if (string.IsNullOrEmpty(assemblyDll))
            {
                //return false;
            }

            var file = Path.GetFullPath(assemblyDll);

            if (!File.Exists(file))
            {
                // check framework or dedicated runtime app folder
                var path = Path.GetDirectoryName(typeof(object).Assembly.Location);
                file = Path.Combine(path, assemblyDll);
                if (!File.Exists(file))
                {
                    //   return false;
                }
            }

            //if (References.Any(r => r.FilePath == file))
            //{ 
            //    return true; 
            //}

            try
            {
                //var reference = MetadataReference.CreateFromFile(file);
                //References.Add(reference);
            }
            catch
            {
                //return false;
            }

            //return true;
        }

        public object Compile()
        {
            return null;
        }


        private static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOverflowChecks(true).WithOptimizationLevel(OptimizationLevel.Release)
                    .WithUsings(DefaultNamespaces);

        private static string runtimePath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\{0}.dll";

        private static readonly IEnumerable<MetadataReference> DefaultReferences =
          new[]
          {
                MetadataReference.CreateFromFile(string.Format(runtimePath, "mscorlib")),
                MetadataReference.CreateFromFile(string.Format(runtimePath, "System")),
                MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Core"))
          };

        public static SyntaxTree Parse(string text, string filename = "", CSharpParseOptions options = null)
        {
            var stringText = SourceText.From(text, Encoding.UTF8);
            return SyntaxFactory.ParseSyntaxTree(stringText, options, filename);
        }

        public DeNetLibCompilerResults Eval(string source, string type, string method)
        {
            //Create assembly from source.
            var parsedSyntaxTree = Parse(source, "", CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp5));

            string assemblyToCreate = $"Test-{Guid.NewGuid().ToString()}.dll";
            string path = @"C:\temp";
            var compilation = CSharpCompilation.Create(assemblyToCreate, new SyntaxTree[] { parsedSyntaxTree }, DefaultReferences, DefaultCompilationOptions);
            string assemblyFullQualifiedFilePath = $@"{path}\{assemblyToCreate}";
            var result = compilation.Emit(assemblyFullQualifiedFilePath);

            CompilerErrorCollection compilerErrorCollection = new CompilerErrorCollection();

            DeNetLibCompilerResults deNetLibCompilerResults = Compiler.CreateResult(result);
            //Diagnostic messages
            IEnumerable<Diagnostic> failures = result.Diagnostics;
            foreach (Diagnostic diagnostic in failures)
            {
                string diagnosticMessage = $"{diagnostic.Id}: {diagnostic.GetMessage()}";
                deNetLibCompilerResults.CustomMessages.Add(diagnosticMessage);
                Console.Error.WriteLine(diagnosticMessage);

                if (!diagnostic.IsSuppressed)
                {
                    if (diagnostic.Severity == DiagnosticSeverity.Error || 
                        diagnostic.IsWarningAsError && (diagnostic.Severity == DiagnosticSeverity.Error || diagnostic.Severity == DiagnosticSeverity.Warning))
                    {
                        CompilerError compilerError = new CompilerError();
                        compilerError.ErrorText = diagnostic.GetMessage();
                        compilerError.ErrorNumber = diagnostic.Id;
                        compilerError.IsWarning = diagnostic.Severity == DiagnosticSeverity.Warning;
                        if (diagnostic.Location == null) continue;
                        compilerError.Line = diagnostic.Location.GetLineSpan().StartLinePosition.Line + 1;
                        compilerError.Column = diagnostic.Location.GetLineSpan().StartLinePosition.Character + 1;

                        compilerError.FileName = diagnostic.Location.SourceTree.FilePath;
                        compilerErrorCollection.Add(compilerError);
                    }
                }
            }

            deNetLibCompilerResults.EmitResult = !(compilerErrorCollection.Count > 0);


            if (compilerErrorCollection != null && compilerErrorCollection.Count > 0)
            {
                CompilerErrorEventArgs compilerErrorEventArgs = new CompilerErrorEventArgs(compilerErrorCollection);
                deNetLibCompilerResults.CompilerErrors = compilerErrorCollection;
            }

            if (deNetLibCompilerResults.EmitResult)
            {
                //Execute
                Assembly assembly = Assembly.LoadFile(assemblyFullQualifiedFilePath);
                Type typeToCreate = assembly.GetType(type);
                object objectToCreate = Activator.CreateInstance(typeToCreate);
                object objectResult = typeToCreate.InvokeMember(method,
                    BindingFlags.Default | BindingFlags.InvokeMethod,
                    null,
                    objectToCreate,
                    new object[] { });

                deNetLibCompilerResults.PathToAssembly = assembly.GetFilePath();
                deNetLibCompilerResults.CompiledAssembly = assembly;
                deNetLibCompilerResults.CSharpCompilation = compilation;
                deNetLibCompilerResults.ClassObjectCreatedFromType = objectToCreate;
                deNetLibCompilerResults.ResultObject = objectResult;
            }
            return deNetLibCompilerResults;
        }
        
        public T Eval<T>(string source, string type, string method)
        {
            object result = Eval(source, type, method);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public DeNetLibCompilerResults Eval(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] _aParam)
        {
            return null;
        }

        public void ResetUsings()
        {
        }

        void ICompilerBehavior.AddDefaultUsings()
        {
            var rtPath = Path.GetDirectoryName(typeof(object).Assembly.Location) +
                         Path.DirectorySeparatorChar;

            //TODO:
            //AddUsings(
            //    rtPath + "System.Private.CoreLib.dll",
            //    rtPath + "System.Runtime.dll",
            //    rtPath + "System.Console.dll",
            //    rtPath + "netstandard.dll",

            //    rtPath + "System.Text.RegularExpressions.dll", // IMPORTANT!
            //    rtPath + "System.Linq.dll",
            //    rtPath + "System.Linq.Expressions.dll", // IMPORTANT!

            //    rtPath + "System.IO.dll",
            //    rtPath + "System.Net.Primitives.dll",
            //    rtPath + "System.Net.Http.dll",
            //    rtPath + "System.Private.Uri.dll",
            //    rtPath + "System.Reflection.dll",
            //    rtPath + "System.ComponentModel.Primitives.dll",
            //    rtPath + "System.Globalization.dll",
            //    rtPath + "System.Collections.Concurrent.dll",
            //    rtPath + "System.Collections.NonGeneric.dll",
            //    rtPath + "Microsoft.CSharp.dll"
            //);

            // this library and CodeAnalysis libs
            //AddUsings(typeof(ReferenceList)); // Scripting Library
        }

        /// <summary>
        /// Raises the <see cref="E:CompilerError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="CompilerErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerError(CompilerErrorEventArgs e)
        {
            CompilerErrorEventHandler compilerError = this.CompilerError;
            this.CompilerError?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:CompilerSuccess" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerSuccess(CompilerSuccessEventArgs e)
        {
            EventHandler compilerSuccess = this.CompilerSuccess;
            compilerSuccess?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:TypeCreationError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TypeCreationErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTypeCreationError(TypeCreationErrorEventArgs e)
        {
            this.TypeCreationError?.Invoke(this, e);
        }

        public T Eval<T>(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] _aParam)
        {
            throw new NotImplementedException();
        }
    }
}
