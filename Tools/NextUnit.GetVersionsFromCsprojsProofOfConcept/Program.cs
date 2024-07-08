// See https://aka.ms/new-console-template for more information
using NextUnit.AssemblyReader.ReferenceReader;
using System.Xml;
using NextUnit.Core.Extensions;

string folderPath = @"C:";
string[] csprojFiles = Directory.GetFiles(folderPath, "*.csproj", SearchOption.AllDirectories);

Dictionary<string, Dictionary<string, List<string>>> referencesByVersion = ReferenceReader.ReferencesByVersion;

Console.WriteLine(
@"Detecting differences.
Please wait...
");

foreach (string csprojFile in csprojFiles)
{
    string projectName = Path.GetFileNameWithoutExtension(csprojFile);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(csprojFile);

    // Handle both Reference and PackageReference elements
    XmlNodeList referenceNodes = xmlDocument.GetElementsByTagName("Reference");
    XmlNodeList packageReferenceNodes = xmlDocument.GetElementsByTagName("PackageReference");

    ReferenceReader.ProcessReferenceNodes(referenceNodes, projectName, referencesByVersion);
    ReferenceReader.ProcessReferenceNodes(packageReferenceNodes, projectName, referencesByVersion, true);
}

// Output information, including source projects for references with multiple versions
foreach (var reference in referencesByVersion)
{
    if (reference.Value.Count > 1) // Multiple versions exist
    {
        $"<Green>Reference Name: {reference.Key}</Green>".WriteColoredLine();
        "<White>Versions:</White>".WriteColoredLine();
        foreach (var version in reference.Value)
        {
            string projects = string.Join(", ", version.Value);
            $"<Blue>{version.Key} [{projects}]</Blue>".WriteColoredLine();
        }
        Console.WriteLine();
    }
}

Console.ReadLine();