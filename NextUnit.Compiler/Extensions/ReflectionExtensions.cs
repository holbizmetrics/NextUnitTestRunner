using System.Reflection;

namespace NextUnit.Compiler.Extensions
{
    public static class ReflectionExtensions
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string GetFilePath(this Assembly assembly)
        {
            return assembly.Location;
        }
    }
}
