using Microsoft.CodeAnalysis.CSharp;
using System.CodeDom.Compiler;

namespace NextUnit.Compiler
{
    public class DeNetLibCompilerResults : CompilerResults
    {
        public List<string> CustomMessages { get; } = new List<string>();
        public bool EmitResult { get; internal set; } = false;
        public CSharpCompilation CSharpCompilation { get; internal set; } = null;
        public object ResultObject { get; internal set; } = null;
        public DeNetLibCompilerResults(TempFileCollection tempFiles) : base(tempFiles)
        {
        }

        public CompilerErrorCollection CompilerErrors { get; internal set; }
        public object ClassObjectCreatedFromType { get; internal set; } = null;
    }
}
