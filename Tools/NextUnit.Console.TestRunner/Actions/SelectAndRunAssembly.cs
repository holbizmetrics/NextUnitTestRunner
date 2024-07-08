using NextUnit.Core.Extensions;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.Console.TestRunner.Actions
{
	public class SelectAndRunAssembly : ITestRunner5ConsoleAction
	{
		public void Run(ITestRunner5 testRunner)
		{
			// Your logic for the user to select a test assembly to run
			"<Green>Select a test assembly to run:</Green>".WriteColoredLine();
			// Implementation...

			// Example of listing assemblies and selecting one to run
			int i = 0;
			IEnumerable<string> testDLLs = Helper.TestDLLs;
			foreach (string testDll in testDLLs)
			{
				System.Console.WriteLine($"{++i}. {testDll}");
			}
			// Wait for user input and run the selected assembly
			var number = System.Console.ReadLine();
			bool gotANumber = int.TryParse(number, out i);
			if (!gotANumber) return;
			testRunner.Run(testDLLs.Skip(i - 1).First());
		}
	}
}
