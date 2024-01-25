using System.Runtime.Loader;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// If another context might be needed also managing unmanaged DLLs.
    /// </summary>
    internal sealed class TestRunnerAssemblyLoadContext : AssemblyLoadContext
    {
        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName); return base.LoadUnmanagedDll(unmanagedDllName);
        }
    }
}
