using System.Reflection;
using System.Runtime.Loader;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// If another context might be needed also managing unmanaged DLLs.
    /// 
    /// To free the resources used TestRunnerAssemblyLoadContextInstance.Unload()
    /// </summary>
    internal sealed class TestRunnerAssemblyLoadContext : AssemblyLoadContext
    {
        public TestRunnerAssemblyLoadContext()
            : base(isCollectible: true)
        {
        }

        protected override Assembly Load(AssemblyName assemblyName) => null;

        public bool UseBase { get; set; } = false;
        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            if (UseBase)
            {
                return base.LoadUnmanagedDll(unmanagedDllName);
            }
            return LoadUnmanagedDllFromPath(unmanagedDllName); ;
        }
    }
}
