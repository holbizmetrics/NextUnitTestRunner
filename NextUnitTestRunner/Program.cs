// See https://aka.ms/new-console-template for more information
using NextUnitTestRunner;
using NextUnitTestRunner.TestClasses;
using System.Diagnostics;

Trace.Listeners.Add(new ConsoleTraceListener());
TestRunner2 testRunner = new TestRunner2();
testRunner.AfterTestRun += TestRunner_AfterTestRun;
testRunner.BeforeTestRun += TestRunner_BeforeTestRun;
testRunner.TestExecuting += TestRunner_TestExecuting;
testRunner.TestRunStarted += TestRunner_TestRunStarted;
testRunner.TestRunFinished += TestRunner_TestRunFinished;
testRunner.ErrorEventHandler += TestRunner_ErrorEventHandler;

void TestRunner_ErrorEventHandler(object sender, ExecutionEventArgs e)
{
    Trace.WriteLine($"Test execution error:{e.TestResult.StackTrace}");
}

void TestRunner_TestRunFinished(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");
    Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
    Trace.WriteLine("");

    Trace.WriteLine(NextUnitTestExecutionContext.ToString());
}

void TestRunner_TestRunStarted(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");
    Trace.WriteLine(NextUnitTestEnvironmentContext.ToString());
    Trace.WriteLine("");

    Trace.WriteLine(NextUnitTestExecutionContext.ToString());
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
    Trace.WriteLine("");
}


testRunner.Run(typeof(TestClass));


Trace.WriteLine("Test run finished.");