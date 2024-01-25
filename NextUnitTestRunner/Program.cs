// See https://aka.ms/new-console-template for more information
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.TestRunner;
using NextUnit.TestRunnerTests;
using System.Diagnostics;

Trace.Listeners.Add(new ConsoleTraceListener());
ITestRunner3 testRunner = new TestRunner3();
testRunner.AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper();
testRunner.AfterTestRun += TestRunner_AfterTestRun;
testRunner.BeforeTestRun += TestRunner_BeforeTestRun;
testRunner.TestExecuting += TestRunner_TestExecuting;
testRunner.TestRunStarted += TestRunner_TestRunStarted;
testRunner.TestRunFinished += TestRunner_TestRunFinished;
testRunner.ErrorEventHandler += TestRunner_ErrorEventHandler;

TestRunnerTestsContainer2 testRunnerTestsContainer2 = new TestRunnerTestsContainer2();
//Run for one type or so:
//testRunner.Run(typeof(TestClass));


testRunner.Run(testRunnerTestsContainer2);
/*string fileName = @"C:\Users\MOH1002\source\repos\NextUnitTestRunner\NextUnitTestRunnerTests\bin\Debug\net8.0\NextUnitTestRunnerTests.dll";
if (Directory.Exists(fileName))
{
    Trace.WriteLine($"Error: {fileName} is a directory and not a file");
}
else if (!File.Exists(fileName))
{
    Trace.WriteLine($"Error: TestRun for {fileName} cannot be started because {fileName} cannot be found.");
}
else
{
    testRunner.Run(fileName);
    Trace.WriteLine("Test run finished.");
}*/


void TestRunner_ErrorEventHandler(object sender, ExecutionEventArgs e)
{
    Trace.WriteLine($"Test execution error: {e.ToString()}");
}

void TestRunner_TestRunFinished(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");
    Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
    Trace.WriteLine("");

    Trace.WriteLine(NextUnitTestExecutionContext.ToString());
    Trace.WriteLine("");
}

void TestRunner_TestRunStarted(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");
    Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
    Trace.WriteLine("");

    Trace.WriteLine(NextUnitTestExecutionContext.ToString());
    Trace.WriteLine("");
}

void TestRunner_TestExecuting(object? sender, ExecutionEventArgs e)
{
}

void TestRunner_BeforeTestRun(object? sender, ExecutionEventArgs e)
{
}

void TestRunner_AfterTestRun(object? sender, ExecutionEventArgs e)
{
    Trace.WriteLine(e.TestResult.ToString());
    Trace.WriteLine(@"");
}

