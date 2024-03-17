#define DIAGNOSE_RUN
// See https://aka.ms/new-console-template for more information

using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Console.TestRunner;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner;
using NextUnit.TestRunner.Extensions;
using NextUnit.TestRunner.TestRunners;
using NextUnit.TestRunner.TestRunners.NewFolder;
using NextUnit.TestRunner.UnitTests;
using System.Diagnostics;

//Trace.Listeners.Add(new ConsoleTraceListener());

EventHandlings eventHandlings = new EventHandlings();

ITestRunner5 testRunner = new TestRunner5().With(new TestDiscoverer()).With(new AutofixtureAutomoqAttributeAttributeLogicMapper());
testRunner.UseCombinator = false;
testRunner.AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper();
testRunner.AfterTestRun += eventHandlings.TestRunner_AfterTestRun;
testRunner.BeforeTestRun += eventHandlings.TestRunner_BeforeTestRun;
testRunner.TestExecuting += eventHandlings.TestRunner_TestExecuting;
testRunner.TestRunStarted += eventHandlings.TestRunner_TestRunStarted;
testRunner.TestRunFinished += eventHandlings.TestRunner_TestRunFinished;
//testRunner.ErrorEventHandler += eventHandlings.TestRunner_ErrorEventHandler;

#if DIAGNOSE_RUN
if (!Trace.Listeners.Contains(new ConsoleTraceListener()))
{
    Trace.Listeners.Add(new ConsoleTraceListener());
}
#endif

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
        Console.WriteLine("--------------------------------------------------------------");
        $"Now running tests for <CYAN>{Path.GetFileName(testDLL)}</CYAN>".WriteColoredLine();
        Console.WriteLine("--------------------------------------------------------------");
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


public class TestConstructorParametersAssert
{   
    public TestConstructorParametersAssert(int param1, bool param2)
    {

    }

    public TestConstructorParametersAssert(TestConstructorParametersAssert param1, TestConstructorParametersAssert param2)
    {

    }
}
