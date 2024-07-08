using NextUnit.Core.Extensions;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.Console.TestRunner.Actions
{
	public class EnterAndRunTestAssembly : ITestRunner5ConsoleAction
	{
		public void Run(ITestRunner5 testRunner)
		{
			// Your logic for the user to enter a test assembly path to run
			"<Green>Enter the path of the test assembly to run:</Green>".WriteColoredLine();
			var path = System.Console.ReadLine();
			// Validate and run the entered path
			if (File.Exists(path))
			{
				System.Console.WriteLine($"Running tests in {path}...");
				testRunner.Run(path);
			}
			else
			{
				System.Console.WriteLine("File not found.");
			}
		}
	}
}
