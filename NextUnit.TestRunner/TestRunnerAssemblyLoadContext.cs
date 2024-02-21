using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// If another context might be needed also managing unmanaged DLLs.
    /// 
    /// To free the resources used TestRunnerAssemblyLoadContextInstance.Unload()
    /// </summary>
    public sealed class TestRunnerAssemblyLoadContext : AssemblyLoadContext, IDisposable
    {
        private bool disposedValue;

        public TestRunnerAssemblyLoadContext()
            : base(isCollectible: true)
        {
        }

        public void Unload()
        {
            if (this.IsCollectible)
            {
                this.Unload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyLoadContext"></param>
        public static void UnloadAssemblyContext(AssemblyLoadContext assemblyLoadContext)
        {
            if (AssemblyLoadContext.Default.IsCollectible)
            {
                assemblyLoadContext.Unload();
            }
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

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Unload();

                }

                if (IsCollectible)
                {
                    Collect();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        private void Collect()
        {
            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Optional: Monitor with WeakReference to confirm unload
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Call from outside to see if it worked reliably.
        /// </summary>
        /// <param name="assemblyLoadContext"></param>
        public bool IsWeakReferenceAlive(AssemblyLoadContext assemblyLoadContext)
        {
            WeakReference weakReference = new WeakReference(assemblyLoadContext);
            return !weakReference.IsAlive ? true : false;
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TestRunnerAssemblyLoadContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
