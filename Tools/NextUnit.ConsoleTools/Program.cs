// See https://aka.ms/new-console-template for more information
using NextUnit.Core;
using NextUnit.TestGenerator;
using System.Reflection;
using NextUnit.ConsoleTools;
using NextUnit.Core.Extensions;

//AssemblyResolver.ActivateEventHandlers();

AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

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
	assembly = TestRunnerAssemblyLoadContext.Default.LoadFromAssemblyPath(source);
}
catch (BadImageFormatException ex)
{
	Console.WriteLine(ex);
}
catch (Exception ex)
{
	Console.WriteLine(ex);
}

OutputGenerator outputFormat = null;
try
{
	outputFormat = stubGenerator.Create(assembly.GetLoadableTypes().ToArray());
	Console.WriteLine(outputFormat.Create());
}
catch (Exception ex)
{
	Console.WriteLine(ex);
}
Console.WriteLine(outputFormat.Create());

Console.ReadLine();

void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
	Console.WriteLine(e.ExceptionObject);
	Console.ReadLine();
}
