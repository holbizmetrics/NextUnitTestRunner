using Microsoft.DiaSymReader;
using System.Diagnostics;
using System.Reflection;

namespace NextUnit.AssemblyReader
{
    /// <summary>
    /// Helper class to get specific contents of a PDB file.
    /// </summary>
    public static class PdbReader
    {
        //public static string AssemblyShortName => System.IO.Path.GetFileNameWithoutExtension(AssemblyFilePath).ToLowerInvariant();
        /// <summary>
        /// Gets all paths of all documents from the given PDB.
        /// 
        /// Example call:
        /// 
        /// string pdbFileName = @"C:\Users\MOH1002\source\repos\NextUnitTestRunner - Kopie\NextUnitTestRunner\bin\Debug\net8.0\NextUnitTestRunner.pdb";
        /// IReadOnlyCollection<string> documentsFilePaths = PdbReader.GetAllDocumentPathsFromPdb(pdbFileName);
        /// </summary>
        public static IReadOnlyCollection<string> GetAllDocumentPathsFromPdb(string path)
        {
            using var stream = new FileStream(path, FileMode.Open);
            var metadataProvider = new SymReaderMetadataProvider();
            var reader = SymUnmanagedReaderFactory.CreateReader<ISymUnmanagedReader5>(stream, metadataProvider);
            var result = reader.GetDocuments();

            return GetDocumentPaths(result).ToList();
        }

        public static IEnumerable<string> GetAllPDBs(string path)
        {
            string pdbExtension = "*.pdb";
            return Directory.GetFiles(path, pdbExtension, SearchOption.AllDirectories);
        }

        public static string GetPDBNameFromAssemblyName(string assemblyName)
        {
            string pdbName = assemblyName.Replace(".dll", ".pdb").Replace(".exe", ".pdb");
            return pdbName;
        }

        public static string GetCallingAssemblyPath => Assembly.GetCallingAssembly().Location;
        public static string GetEntryAssemblyPath => Assembly.GetEntryAssembly().Location;
        public static string GetExecutingAssemblyPath => Assembly.GetExecutingAssembly().Location;
  
        public static IEnumerable<string> GetAllSourceFiles(string path)
        {
            List<string> sourceFiles = new List<string>();
            IEnumerable<string> pdbs = GetAllPDBs(path);
            foreach( var pdb in pdbs)
            {
                sourceFiles.AddRange(GetAllDocumentPathsFromPdb(pdb));
            }
            return sourceFiles;
        }

        public static void CreatePDBCopiesInTemporaryDirectory(string pdbFileFullyQualifiedPath, string temppath)
        {
            File.Copy(pdbFileFullyQualifiedPath, temppath, true); 
        }

        /// <summary>
        /// Gets all the assemblies from solution top level directory.
        /// </summary>
        /// <param name="combine"></param>
        /// <returns></returns>
        //public static string[] GetAllAssembliesFromSolutionTopLevelDirectory(string combine = null)
        //{
        //    // Get the full path to the directory containing the executing assembly.
        //    var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        //    var directoryPath = Path.GetDirectoryName(executingAssemblyPath);

        //    // Assuming a standard solution structure, where the solution directory is two levels up from the bin directory.
        //    var solutionDirectoryPath = Path.GetFullPath(Path.Combine(directoryPath, @"..\.."));

        //    string topLevelBinDirectory = string.Empty;
        //    if (!string.IsNullOrEmpty(combine))
        //    {
        //        // Define the path to the solution's top-level bin directory (adjust as necessary).
        //        topLevelBinDirectory = Path.Combine(solutionDirectoryPath, combine);
        //    }

        //    // Check if the directory exists.
        //    if (!Directory.Exists(topLevelBinDirectory))
        //    {
        //        Trace.WriteLine("The top-level bin directory does not exist.");
        //        return Array.Empty<string>();
        //    }

        //    // Get all DLL files in the top-level bin directory and its subdirectories.
        //    var assemblyFiles = Directory.GetFiles(topLevelBinDirectory, "*.dll", SearchOption.AllDirectories);

        //    return assemblyFiles;
        //}

        /// <summary>
        /// Gets the document paths.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetDocumentPaths(IEnumerable<ISymUnmanagedDocument> result)
        {
            foreach (var document in result)
            {
                var url = new char[256];
                document.GetUrl(url.Length, out var count, url);
                yield return new string(url, 0, count - 1);
            }
        }

        /// <summary>
        /// Query the PDF existing in the given file path.
        /// </summary>
        /// <param name="pdbPath"></param>
        public static void QueryPdb(string pdbPath)
        {
            try
            {
                // Create an instance of DiaSource
                Type diaSourceType = Type.GetTypeFromProgID("Dia2Lib.DiaSource");
                object diaSourceObj = Activator.CreateInstance(diaSourceType);
                //IDiaDataSource diaSource = (IDiaDataSource)diaSourceObj;

                // Load the PDB file
                //diaSource.loadDataFromPdb(pdbPath);

                // Open a session
                //IDiaSession diaSession;
                //diaSource.openSession(out diaSession);

                // Get the global scope
                //IDiaSymbol globalScope;
                //diaSession.get_globalScope(out globalScope);

                // Now you can use diaSession and globalScope to query for symbols
                // This is where you would add your logic to work with the symbols

                Trace.WriteLine("PDB queried successfully.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error querying PDB: {ex.Message}");
            }
        }
    }
}
