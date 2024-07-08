using NextUnit.Core.Extensions;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.Console.TestRunner.Actions
{
	public class RunAllTestsSequentially : ITestRunner5ConsoleAction
	{
		public void Run(ITestRunner5 testRunner)
		{
			// Your logic to run all tests sequentially
			System.Console.WriteLine("Running all tests sequentially...");

			//Running tests in sequential manner.
			testRunner.UseThreading = false;

			foreach (string testDLL in Helper.TestDLLs)
			{
				System.Console.WriteLine("--------------------------------------------------------------");
				$"Now running tests for <CYAN>{Path.GetFileName(testDLL)}</CYAN>".WriteColoredLine();
				System.Console.WriteLine("--------------------------------------------------------------");
				try
				{
					testRunner.Run(testDLL);
				}
				catch (Exception ex)
				{
					$"<RED>Error: {ex}</RED>".WriteColoredLine();
				}
				testRunner.Dispose();
			}
		}
	}

	public interface ITestRunner5ConsoleAction
	{
		void Run(ITestRunner5 testRunner);
	}
}
