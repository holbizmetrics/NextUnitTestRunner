using NextUnit.Core.Extensions;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.Console.TestRunner.Actions
{
	public class RunAllTestsSequentiallyXTimes : ITestRunner5ConsoleAction
	{
		public void Run(ITestRunner5 testRunner)
		{
			// Your logic for the user to enter a test assembly path to run
			"<Green>Enter the iterations of the test assemblies to run:</Green>".WriteColoredLine();
			var count = System.Console.ReadLine();

			int maxIterations = 0;
			bool gotANumber = int.TryParse(count, out maxIterations);

			ITestRunner5ConsoleAction testRunner5ConsoleAction = new RunAllTestsSequentially();
			for (int i = 0; i < maxIterations; i++)
			{
				testRunner5ConsoleAction.Run(testRunner);
			}
		}
	}
}