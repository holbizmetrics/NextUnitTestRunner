// See https://aka.ms/new-console-template for more information
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.TestRunner;
using NextUnit.TestRunner.Extensions;
using NextUnit.TestRunner.UnitTests;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

Trace.Listeners.Add(new ConsoleTraceListener());
ITestRunner3 testRunner = new TestRunner3();
testRunner.UseCombinator = false;
testRunner.AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper();
testRunner.AfterTestRun += TestRunner_AfterTestRun;
testRunner.BeforeTestRun += TestRunner_BeforeTestRun;
testRunner.TestExecuting += TestRunner_TestExecuting;
testRunner.TestRunStarted += TestRunner_TestRunStarted;
testRunner.TestRunFinished += TestRunner_TestRunFinished;
testRunner.ErrorEventHandler += TestRunner_ErrorEventHandler;

TestRunnerTestsContainer2 testRunnerTestsContainer2 = new TestRunnerTestsContainer2();
testRunner.Run(testRunnerTestsContainer2);

//Run for one type or so:
//testRunner.Run(typeof(TestClass));

string fileNextUnitTestRunnerTests = @"C:\Users\MOH1002\source\repos\NextUnitTestRunner\NextUnitTestRunnerTests\bin\Debug\net8.0\NextUnit.TestRunnerTests.dll";
//testRunner.Run(fileNextUnitTestRunnerTests);
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
    string errorText =
$@"MethodInfo: <Green>{e.MethodInfo}</Green>
TestResult: <Green>{e.TestResult}</Green>";
    if (e.LastException != null)
    {
        errorText = 
$@"{errorText}

Exception:

<Red>{e.LastException}</Red>";
    };
    $"Test execution error: {errorText}".WriteColoredLine();
}

void TestRunner_TestRunFinished(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");

    string output = GetEnvironmentContext();
    output.WriteColoredLine();
    Trace.WriteLine("");

    Trace.WriteLine(NextUnitTestExecutionContext.ToString());
    Trace.WriteLine("");
}

void TestRunner_TestRunStarted(object sender, ExecutionEventArgs e)
{
    // Show Hardware Snapshots
    Trace.WriteLine("Hardware snapshot:");
    string output = GetEnvironmentContext();
    output.WriteColoredLine();
    Trace.WriteLine("");

    output = GetTestExecutionContext();
    output.WriteColoredLine();
    Trace.WriteLine("");
}

void TestRunner_TestExecuting(object? sender, ExecutionEventArgs e)
{
}

void TestRunner_BeforeTestRun(object? sender, ExecutionEventArgs e)
{
}

string GetEnvironmentContext()
{
    string output =
$@"MachineName: <Green>{NextUnitTestEnvironmentContext.MachineName}</Green>
CommandLine: <Green>{NextUnitTestEnvironmentContext.CommandLine}</Green>
ProcessorCount: <Green>{NextUnitTestEnvironmentContext.ProcessorCount}</Green>
BiosInfo: <Green>{NextUnitTestEnvironmentContext.BiosInfo}</Green>
Capacity: <Green>{NextUnitTestEnvironmentContext.Capacity}</Green>
OperatingSystem: <Green>{NextUnitTestEnvironmentContext.OperatingSystem}</Green>";
    return output;
}

string GetTestExecutionContext()
{
    string output =
$@"TestRunStart: <Green>{NextUnitTestExecutionContext.TestRunStart}</Green>;
TestRunEnd: <Green>{NextUnitTestExecutionContext.TestRunEnd}</Green>
TestRunTime: <Green>{NextUnitTestExecutionContext.TestRunTime}</Green>";
    return output;
}

void TestRunner_AfterTestRun(object? sender, ExecutionEventArgs e)
{
    string testResultStateText = e.TestResult.State switch
    { 
        ExecutedState.Passed => "<Green>passed</Green>",
        ExecutedState.Failed => "<Red>failed</Red>",
        ExecutedState.Skipped => "<Blue>skipped</Blue>",
        ExecutedState.UnknownError => "<Cyan>Unknown Error</Cyan>",
        ExecutedState.NotStarted => "<White>Not started</White>", 
    };
string    output =
$@"MethodInfo: <Blue>{e.MethodInfo}</Blue>
TestResult: {testResultStateText}
DisplayName: <Green>{e.TestResult.DisplayName}</Green>
Class: <Green>{e.TestResult.Class}, Namespace: {e.TestResult.Namespace}</Green>
Start: <Green>{e.TestResult.Start}</Green>
End: <Green>{e.TestResult.End}</Green>
Execution Time: <Green>{e.TestResult.ExecutionTime}</Green>
Workstation: <Green>{e.TestResult.Workstation}</Green>
";

    output.WriteColoredLine();
    Trace.WriteLine(@"");
}

