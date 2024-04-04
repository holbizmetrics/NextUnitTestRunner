// See https://aka.ms/new-console-template for more information
using NextUnit.TestGenerator;
using System.Reflection;

StubGenerator stubGenerator = new StubGenerator();

string source = string.Empty;

if (args != null && args.Length > 0)
{
    source = args[0];
}
else
{
    Console.WriteLine("Please specify an argument (assembly, full qualified file path.");
    return;
}

if (!File.Exists(source))
{
    Console.WriteLine($"The file {source} does not exist.");
    return;
}

if (Directory.Exists(source))
{
    Console.WriteLine($"{source} is a directory and not a file.");
    return;
}

Assembly assembly = null;
try
{
    assembly = Assembly.LoadFrom(source);
}
catch(BadImageFormatException ex)
{
    Console.WriteLine(ex);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}

OutputGenerator outputFormat = stubGenerator.Create(assembly.GetTypes());
Console.WriteLine(outputFormat.Create());
