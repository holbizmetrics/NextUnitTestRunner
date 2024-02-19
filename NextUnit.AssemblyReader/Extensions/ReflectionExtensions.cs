using System.Reflection;
namespace NextUnit.AssemblyReader.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets all the sources for an assembly.
        /// </summary>
        /// <param name="assembly"></param>
        public static IEnumerable<string> GetSources(this Assembly assembly, bool useShadowCopy = true)
        {
            List<string> fileSources = new List<string>();
            if (assembly == null) return null;
            Module[] files = assembly.GetLoadedModules();


            string tempDirectory = @"C:\temp";
            foreach (Module module in files)
            {
                string newName = module.FullyQualifiedName.Replace(".dll", ".pdb").Replace(".exe", ".pdb");
                try
                {
                    PdbReader.CreatePDBCopiesInTemporaryDirectory(newName, Path.Combine(tempDirectory, newName));
                    IReadOnlyCollection<string> moduleFiles = PdbReader.GetAllDocumentPathsFromPdb(newName);
                    fileSources.AddRange(moduleFiles);
                }
                catch (Exception ex)
                {

                }
            }
            return fileSources;
        }
    }
}
