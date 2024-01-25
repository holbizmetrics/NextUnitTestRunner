using System.Runtime.Loader;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace NextUnit.TestRunner
{
    public class AssemblyResolver
    {
        private readonly DependencyContext _dependencyContext;
        private readonly string _basePath;

        public AssemblyResolver(DependencyContext dependencyContext, string basePath)
        {
            _dependencyContext = dependencyContext ?? throw new ArgumentNullException(nameof(dependencyContext));
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        public Assembly Resolve(AssemblyName name)
        {
            // Attempt to resolve the assembly from the dependency context
            var library = _dependencyContext.RuntimeLibraries.FirstOrDefault(l => l.Name.Equals(name.Name, StringComparison.OrdinalIgnoreCase));
            if (library != null)
            {
                var wrapper = new CompilationLibrary(
                    library.Type,
                    library.Name,
                    library.Version,
                    library.Hash,
                    library.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                    new List<Dependency>(),
                    false);

                var assemblyPaths = new List<string>();
                //wrapper.ResolveReferencePaths(assemblyPaths);
                if (assemblyPaths.Count > 0)
                {
                    return TestRunnerAssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(_basePath, assemblyPaths[0]));
                }
            }

            return null;
        }
    }
}