using System.Runtime.Loader;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// If another context might be needed also managing unmanaged DLLs.
    /// </summary>
    internal sealed class TestRunnerAssemblyLoadContext : AssemblyLoadContext
    {
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
