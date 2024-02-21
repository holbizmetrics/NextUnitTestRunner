// See https://aka.ms/new-console-template for more information

//namespace NextUnit.Console.TestRunner;

using NextUnit.AssemblyReader;
using NextUnit.AssemblyReader.Extensions;
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Core;
using NextUnit.Core.Accessors;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner;
using NextUnit.TestRunner.Extensions;
using NextUnit.TestRunner.TestRunners;
using NextUnit.TestRunner.UnitTests;
using System.Diagnostics;
using System.Reflection;

Trace.Listeners.Add(new ConsoleTraceListener());
ITestRunner4 testRunner = new TestRunner4().With(new TestDiscoverer()).With(new AutofixtureAutomoqAttributeAttributeLogicMapper());
testRunner.UseCombinator = false;
testRunner.AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper();
testRunner.AfterTestRun += TestRunner_AfterTestRun;
testRunner.BeforeTestRun += TestRunner_BeforeTestRun;
testRunner.TestExecuting += TestRunner_TestExecuting;
testRunner.TestRunStarted += TestRunner_TestRunStarted;
testRunner.TestRunFinished += TestRunner_TestRunFinished;
testRunner.ErrorEventHandler += TestRunner_ErrorEventHandler;


string[] assemblyPaths = NextUnit.Core.Extensions.ReflectionExtensions.GetAllAssembliesFromSolutionTopLevelDirectory(@"..\..\");
var testDLLs = assemblyPaths.Where(x => x.Contains("NextUnit.") && x.EndsWith(".Tests.dll") && !x.Contains(@"obj\"));

if (testDLLs == null)
{
    "<Red>No tests found. Program exits.</Red>".WriteColoredLine();
}

while (true)
{
    "<Green>Select an action:</Green>".WriteColoredLine();
    Console.WriteLine("1. Run all detected tests sequentially.");
    Console.WriteLine("2. Select a test assembly to run (of the detected tests).");
    Console.WriteLine("3. Enter a test assembly path to run.");
    Console.WriteLine("4. Run TestRunnerTestsContainer2 tests.");
    Console.WriteLine("5. Run the Test Runner for a selected type.");
    Console.WriteLine("6. Run the Test Runner for a all detected tests x times.");
    Console.WriteLine("7. Run the Test Runner for a selected types x times.");
    Console.WriteLine("8. Exit.");

    Console.Write("Enter your choice (1-5): ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            RunAllTestsSequentially(testRunner);
            break;

        case "2":
            SelectAndRunTestAssembly(testRunner);
            break;

        case "3":
            EnterAndRunTestAssembly(testRunner);
            break;

        case "4":
            RunTestContainer2Tests(testRunner);
            break;

        case "5":
            RunForSelectedType(testRunner);
            return;

        case "6":
            RunAllTestsSequentiallyXTimes(testRunner);
            return;

        case "7":
            RunForSelectedAssemblyXTimes(testRunner);
            return;

        case "8":
            Console.WriteLine("Exit.");
            return;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

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
        ExecutionState.Passed => "<Green>passed</Green>",
        ExecutionState.Failed => "<Red>failed</Red>",
        ExecutionState.Skipped => "<Blue>skipped</Blue>",
        ExecutionState.UnknownError => "<Cyan>Unknown Error</Cyan>",
        ExecutionState.NotStarted => "<White>Not started</White>",
        ExecutionState.Running => "<Yellow>Running</Yellow>"
    };
    string output =
    $@"MethodInfo: <Blue>{e.MethodInfo}</Blue>
TestResult: {testResultStateText}
DisplayName: <Green>{e.TestResult.DisplayName}</Green>
Class: <Green>{e.TestResult.Class}, Namespace: {e.TestResult.Namespace}</Green>
Start: <Green>{e.TestResult.Start}</Green>
End: <Green>{e.TestResult.End}</Green>
Execution Time: <Green>{e.TestResult.ExecutionTime}</Green>
Workstation: <Green>{e.TestResult.Workstation}</Green>
";
    if (e.TestResult.State == ExecutionState.NotStarted)
    {

    }
    output.WriteColoredLine();
    Trace.WriteLine(@"");
}

static ITestRunner3 InitializeTestRunner()
{
    // Your existing initialization logic here
    ITestRunner3 testRunner = new TestRunner3(); // Simplified for example
                                                 // Further initialization...
    return testRunner;
}

void RunAllTestsSequentially(ITestRunner3 testRunner)
{
    // Your logic to run all tests sequentially
    Console.WriteLine("Running all tests sequentially...");

    //Running tests in sequential manner.
    testRunner.UseThreading = false;
    foreach (string testDLL in testDLLs)
    {
        testRunner.Run(testDLL);
        testRunner.Dispose();
    }
}

void SelectAndRunTestAssembly(ITestRunner3 testRunner)
{
    // Your logic for the user to select a test assembly to run
    "<Green>Select a test assembly to run:</Green>".WriteColoredLine();
    // Implementation...

    // Example of listing assemblies and selecting one to run
    int i = 0;
    foreach (string testDll in testDLLs)
    {
        Console.WriteLine($"{++i}. {testDll}");
    }
    // Wait for user input and run the selected assembly
    var number = Console.ReadLine();
    bool gotANumber = int.TryParse(number, out i);
    if (!gotANumber) return;
    testRunner.Run(testDLLs.Skip(i-1).First());
}

void EnterAndRunTestAssembly(ITestRunner3 testRunner)
{
    // Your logic for the user to enter a test assembly path to run
    "<Green>Enter the path of the test assembly to run:</Green>".WriteColoredLine();
    var path = Console.ReadLine();
    // Validate and run the entered path
    if (File.Exists(path))
    {
        Console.WriteLine($"Running tests in {path}...");
        testRunner.Run(path);
    }
    else
    {
        Console.WriteLine("File not found.");
    }
}

void RunTestContainer2Tests(ITestRunner3 testRunner)
{
    TestRunnerTestsContainer2 testRunnerTestsContainer2 = new TestRunnerTestsContainer2();
    $"<Green>Running the tests of {testRunnerTestsContainer2}".WriteColoredLine();

    testRunner.Run(testRunnerTestsContainer2);
}

void RunForSelectedType(ITestRunner3 testRunner)
{
    Console.WriteLine("This won't work on your system, yet.");
}

void RunAllTestsSequentiallyXTimes(ITestRunner3 testRunner)
{
    // Your logic for the user to enter a test assembly path to run
    "<Green>Enter the iterations of the test assemblies to run:</Green>".WriteColoredLine();
    var count = Console.ReadLine();

    int maxIterations = 0;
    bool gotANumber = int.TryParse(count, out maxIterations);
    for (int i = 0;i< maxIterations;i++)
    {
        RunAllTestsSequentially(testRunner);
    }
}

void RunForSelectedAssemblyXTimes(ITestRunner3 testRunner)
{

}

public class TestMarkedPropertiesAttribute : Attribute
{
    [NextUnitValue]
    public int PropertyToTestIfItIsMarked { get; set; }
}
