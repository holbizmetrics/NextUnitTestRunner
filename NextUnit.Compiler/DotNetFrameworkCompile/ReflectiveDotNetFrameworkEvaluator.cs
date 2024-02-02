using NextUnit.Compiler.CompileCore;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NextUnit.Compiler
{
    public class ReflectiveDotNetFrameworkEvaluator : BaseEvaluator
    {
        private Dictionary<string, string> dictionary = new Dictionary<string, string> 
        { 
            {"CompilerVersion",
            "v4.0"}
        };

        private CompilerParameters m_CompilerParameters;
        private CodeDomProvider m_CSharpCodeProvider;
        private ICodeCompiler m_ICodeCompiler;
        private string m_FormulaTemplateClass;
        private CompilerResults results;
        private Assembly m_LastBuiltAssembly;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        //Original
        /*public ReflectiveEvaluator() : this(new CSharpCodeProvider(dictionary1))
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string> {
                { 
                    "CompilerVersion",
                    "v4.0"
                }
            };
        }*/

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectiveEvaluator"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the s assembly.</param>
        public ReflectiveDotNetFrameworkEvaluator(string assemblyName = null)
            : this(new CSharpCodeProvider(), assemblyName)
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectiveDotNetFrameworkEvaluator"/> class.
        /// </summary>
        /// <param name="codeDomProvider">The code DOM provider.</param>
        /// <param name="assemblyName">Name of the s assembly.</param>
        public ReflectiveDotNetFrameworkEvaluator(CodeDomProvider codeDomProvider, string assemblyName = null)
        {
            this.results = null;
            Dictionary<string, string> dictionary1 = new Dictionary<string, string> {
                { 
                    "CompilerVersion",
                    "v4.0"
                }
            };
            this.dictionary = dictionary1;
            this.m_CSharpCodeProvider = null;
            this.m_ICodeCompiler = null;
            this.m_CompilerParameters = null;
            this.m_FormulaTemplateClass = CResources.TemplateClass;
            this.m_CSharpCodeProvider = codeDomProvider;
            this.m_ICodeCompiler = this.m_CSharpCodeProvider.CreateCompiler();
            this.m_CompilerParameters = new CompilerParameters();
            this.m_CompilerParameters.GenerateInMemory = true;
            if (!string.IsNullOrEmpty(assemblyName))
            {
                this.m_CompilerParameters.OutputAssembly = assemblyName;
            }
            this.m_CompilerParameters.GenerateExecutable = false;

            this.m_CompilerParameters.ReferencedAssemblies.Add("system.core.dll");
            this.m_CompilerParameters.ReferencedAssemblies.Add("system.dll");
            this.m_CompilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
        }

        /// <summary>
        /// Adds the default usings.
        /// </summary>
        public void AddDefaultUsings()
        {
            AddUsing("system.core.dll");
            AddUsing("System.dll");
            AddUsing("mscorlib.dll");            
        }

        public void AddUsing(string name)
        {
            AddUsings(name);
        }

        /// <summary>
        /// Adds the using.
        /// </summary>
        /// <param name="referenceNames">The sa reference names.</param>
        public void AddUsings(params string[] referenceNames)
        {
            foreach (string referenceName in referenceNames)
            {
                this.m_CompilerParameters.ReferencedAssemblies.Add(referenceName);
            }
        }

        /// <summary>
        /// Only allow to get default back. Completely empty is not possible.
        /// </summary>
        public void ResetUsings()
        {
            this.m_CompilerParameters.ReferencedAssemblies.Clear();
            AddDefaultUsings();
        }

        /// <summary>
        /// Evals the specified s source.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <param name="type">Type of the s.</param>
        /// <param name="method">The s method.</param>
        /// <returns></returns>
        public object Eval(string source, string type, string method)
        {
            return this.Eval(source, type, method, true, true, null);
        }

        /// <summary>
        /// Evals the specified s source.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <param name="_sType">Type of the s.</param>
        /// <param name="_sMethod">The s method.</param>
        /// <param name="recompileNeeded">if set to <c>true</c> [b recompile needed].</param>
        /// <param name="executeWhenCompileFailed">if set to <c>true</c> [b execute when compile failed].</param>
        /// <param name="parameters">a parameter.</param>
        /// <returns></returns>
        public object Eval(string source, string _sType, string _sMethod, bool recompileNeeded = true, bool executeWhenCompileFailed = true, params object[] parameters)
        {
            bool bHasErrors = false;
            if (recompileNeeded || (this.results == null))
            {
                this.results = this.RuntimeCompileClass(source);
                if (this.results.Errors.HasErrors)
                {
                    bHasErrors = this.results.Errors.HasErrors;
                    this.OnCompilerError(new CompilerErrorEventArgs(this.results.Errors, source));
                    return typeof(void);
                }

                m_LastBuiltAssembly = this.results.CompiledAssembly;
                this.OnCompilerSuccess(new CompilerSuccessEventArgs(m_LastBuiltAssembly));
            }
            if (!executeWhenCompileFailed) return typeof(void);

            Assembly compiledAssembly = this.results.CompiledAssembly;
            if (compiledAssembly == null)
            {
                this.OnCompilerError(new CompilerErrorEventArgs(this.results.Errors, source));
            }
            Type type = compiledAssembly.GetType(_sType);
            if (type != null)
            {
                object obj3 = Activator.CreateInstance(type);
                return obj3.GetType().GetMethod(_sMethod).Invoke(obj3, parameters);
            }
            this.OnTypeCreationError(new TypeCreationErrorEventArgs(_sType));
            return null;
        }

        /*  public static IEnumerable<MethodInfo> GetExtensionMethods(Assembly assembly, Type extendedType) => 
              (from type in assembly.GetTypes()
                  where (type.IsSealed && !type.IsGenericType) && !type.IsNested
                  from method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                  where method.IsDefined(typeof(ExtensionAttribute), false)
                  where method.GetParameters()[0].ParameterType == extendedType
                  select method);*/

        /// <summary>
        /// Raises the <see cref="E:CompilerError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="CompilerErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerError(CompilerErrorEventArgs e)
        {
            CompilerErrorEventHandler compilerError = this.CompilerError;
            if (this.CompilerError != null)
            {
                this.CompilerError(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CompilerSuccess" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerSuccess(CompilerSuccessEventArgs e)
        {
            EventHandler compilerSuccess = this.CompilerSuccess;
            if (compilerSuccess != null)
            {
                compilerSuccess(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:TypeCreationError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TypeCreationErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTypeCreationError(TypeCreationErrorEventArgs e)
        {
            if (this.TypeCreationError != null)
            {
                this.TypeCreationError(this, e);
            }
        }

        /// <summary>
        /// Runtimes the compile class.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <returns></returns>
        private CompilerResults RuntimeCompileClass(string source)
        {
            return this.m_ICodeCompiler.CompileAssemblyFromSource(this.m_CompilerParameters, source);
        }

        public T Eval<T>(string source, string type, string method)
        {
            throw new NotImplementedException();
        }

        public T Eval<T>(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] _aParam)
        {
            throw new NotImplementedException();
        }

        public Assembly LastBuiltAssembly
        {
            get { return m_LastBuiltAssembly; }
        }
    }
}