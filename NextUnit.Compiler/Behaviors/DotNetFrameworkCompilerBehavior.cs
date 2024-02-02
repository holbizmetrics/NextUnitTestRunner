using NextUnit.Compiler.CompileCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NextUnit.Compiler.Behaviors
{
    public class DotNetFrameworkCompilerBehavior : ICompilerBehavior
    {
        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        private ReflectiveDotNetFrameworkEvaluator reflectiveEvaluator = new ReflectiveDotNetFrameworkEvaluator();
        public void AddUsing(string name)
        {
            reflectiveEvaluator.AddUsing(name);
        }

        public object Compile()
        {
            return reflectiveEvaluator.Eval("", "", "");
        }

        public DeNetLibCompilerResults Eval(string source, string type, string method)
        {
            object result = reflectiveEvaluator.Eval(source, type, method);
            return Compiler.CreateResult(result);
        }

        public DeNetLibCompilerResults Eval(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] parameters)
        {
            object result = reflectiveEvaluator.Eval(source, referringType, method, recompileNeeded, _bExecuteWhenCompileFailed, parameters);
            return Compiler.CreateResult(result);
        }

        public void ResetUsings()
        {
            reflectiveEvaluator.ResetUsings();
        }

        void ICompilerBehavior.AddDefaultUsings()
        {
            AddUsing("system.core.dll");
            AddUsing("System.dll");
            AddUsing("mscorlib.dll");
        }

        public T Eval<T>(string source, string type, string method)
        {
            object result = Eval(source, type, method);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T Eval<T>(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] parameters)
        {
            object result = Eval(source, referringType, method, recompileNeeded, _bExecuteWhenCompileFailed, parameters);
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

}
