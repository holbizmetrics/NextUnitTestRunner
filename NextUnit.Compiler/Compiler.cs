using NextUnit.Compiler.Behaviors;
using NextUnit.Compiler.CompileCore;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace NextUnit.Compiler
{
    public class Compiler
    {
        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        public ICompilerBehavior CompilerBehavior { get; set; } = new DotNetCoreCompilerBehavior();

        /// <summary>
        /// If false an assembly will be created, if not the type(s) created will be used.
        /// </summary>
        public bool InMemoryCompile { get; set; } = false;

        public Compiler(ICompilerBehavior compilerBehavior)
        {
            CompilerBehavior = compilerBehavior;
        }

        /// <summary>
        /// Only allow to get default back. Completely empty is not possible.
        /// </summary>
        public void ResetUsings()
        {
            CompilerBehavior.AddDefaultUsings();
        }

        protected virtual void OnCompilerError()
        {
        }

        protected virtual void OnCompilerSuccess()
        {

        }

        public object Eval(string source, string type, string method)
        {
            return CompilerBehavior.Eval(source, type, method);
        }

        public object Eval(string source, string referringType, string method, bool recompileNeeded = true, bool executeWhenCompileFailed = true, params object[] parameters)
        {
            return CompilerBehavior.Eval(source, referringType, method, recompileNeeded, executeWhenCompileFailed, parameters);
        }

        public void AddUsing(string usingReference)
        {
            CompilerBehavior.AddUsing(usingReference);
        }

        /// <summary>
        /// Adds the using.
        /// </summary>
        /// <param name="referenceNames">The sa reference names.</param>
        public void AddUsings(params string[] referenceNames)
        {
            CompilerBehavior.AddUsings(referenceNames);
        }

        /// <summary>
        /// Raises the <see cref="E:TypeCreationError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TypeCreationErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTypeCreationError(TypeCreationErrorEventArgs e)
        {
            if (TypeCreationError != null)
            {
                TypeCreationError(this, e);
            }
        }

        /// <summary>
        /// Compile
        /// </summary>
        public DeNetLibCompilerResults Compile()
        {
            string templateClass = CResources.TemplateClass;
            DeNetLibCompilerResults returnValue = CompilerBehavior.Eval(templateClass, GetType(templateClass), GetMethods(templateClass)[0]);

            //if()
            //{
                //TypeCreationError.Invoke()
            //}
            if (returnValue.EmitResult)
            {
                CompilerSuccess?.Invoke(this, new CompilerSuccessEventArgs());
            }
            else
            {
                CompilerError?.Invoke(this, new CompilerErrorEventArgs(returnValue.CompilerErrors));
            }

            return returnValue;
        }

        public List<string> GetMethods(string templateClass)
        {
            string regexPatternGetMethodNames = @"(public static|private static|public|private|protected)([ \t]+)(\w+)([ \t]+)(\w+)\((.*)\)";
            MatchCollection matches = Regex.Matches(templateClass, regexPatternGetMethodNames);
            List<string> methodNames = matches.Cast<Match>().Select(x => x.Groups[5].ToString()).ToList();
            return methodNames;
        }

        public string GetType(string templateClass)
        {
            string regexPatternGetType = @"(class[ \t]+)(\w+)";
            Match match = Regex.Match(templateClass, regexPatternGetType);
            string type = match.Groups[2].ToString();
            return type;
        }

        public static DeNetLibCompilerResults CreateResult(object result, params string[] addTempFiles)
        {
            TempFileCollection tempFileCollection = new TempFileCollection();
            foreach (string tempFile in addTempFiles)
            {
                tempFileCollection.AddFile(tempFile, true);
            }
            DeNetLibCompilerResults compilerResults = new DeNetLibCompilerResults(tempFileCollection);
            compilerResults.ResultObject = result;
            //compilerResults.CompiledAssembly = reflectiveEvaluator.LastBuiltAssembly;
            //compilerResults.PathToAssembly = reflectiveEvaluator.LastBuiltAssembly.FullName;
            compilerResults.NativeCompilerReturnValue = 0;
            return compilerResults;
        }
    }
}

