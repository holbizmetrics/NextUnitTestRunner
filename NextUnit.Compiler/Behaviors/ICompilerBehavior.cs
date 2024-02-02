using NextUnit.Compiler.CompileCore;

namespace NextUnit.Compiler.Behaviors
{
    public interface ICompilerBehavior
    {
        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        public void AddUsing(string name);
        public void AddUsings(params string[] usings)
        {
            foreach(var usingReference in usings)
            {
                AddUsing(usingReference);
            }
        }
        abstract void AddDefaultUsings();
        abstract void ResetUsings();

        public DeNetLibCompilerResults Eval(string source, string type, string method);
        public T Eval<T>(string source, string type, string method);
        public DeNetLibCompilerResults Eval(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] _aParam);
        public T Eval<T>(string source, string referringType, string method, bool recompileNeeded = true, bool _bExecuteWhenCompileFailed = true, params object[] _aParam);
        
        //public T Compile<T>();
    }
}
