//#define DIAGNOSE_RUN
// See https://aka.ms/new-console-template for more information

//Trace.Listeners.Add(new ConsoleTraceListener());
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Console.TestRunner;
using NextUnit.Console.TestRunner.Actions;
using NextUnit.Console.TestRunner.EventDisplays;
using NextUnit.Core.Extensions;
using NextUnit.TestRunner;
using NextUnit.TestRunner.TestRunners.NewFolder;

ITestRunner5 testRunner = new TestRunner5()
    .With(new TestDiscoverer())
    .With(new AutofixtureAutomoqAttributeAttributeLogicMapper()); //.With(new DefaultCombinator());

EventHandlings eventHandlings = new EventHandlingsSparseOutput();
eventHandlings.Initialize(testRunner);

#if DIAGNOSE_RUN
if (!Trace.Listeners.Contains(new ConsoleTraceListener()))
{
    Trace.Listeners.Add(new ConsoleTraceListener());
}
#endif

IEnumerable<string> testDLLs = Helper.TestDLLs;
if (testDLLs == null)
{
    "<Red>No tests found. Program exits.</Red>".WriteColoredLine();
}

while (true)
{
    "<Green>Select an action:</Green>".WriteColoredLine();
    Console.WriteLine();
    Console.WriteLine("1. Run all detected tests sequentially.");
    Console.WriteLine("2. Select a test assembly to run (of the detected tests).");
    Console.WriteLine("3. Enter a test assembly path to run.");
    Console.WriteLine("4. Run TestRunnerTestsContainer2 tests.");
    Console.WriteLine("5. Run the Test Runner for a selected type.");
    Console.WriteLine("6. Run the Test Runner for a all detected tests x times.");
    Console.WriteLine("7. Run the Test Runner for a selected types x times.");
    Console.WriteLine("8. Exit.");
    Console.WriteLine();
    Console.WriteLine("A. Use sparse testing output");
	Console.WriteLine("B. Use exhaustive testing output");
    Console.WriteLine();
	Console.Write("Enter your choice (1-8, A or B): ");

    var choice = Console.ReadLine();

    ITestRunner5ConsoleAction testRunner5ConsoleAction = null;

    switch (choice)
    {
        case "1":
            testRunner5ConsoleAction = new RunAllTestsSequentially();
            break;

        case "2":
			testRunner5ConsoleAction = new SelectAndRunAssembly();
            break;

        case "3":
            testRunner5ConsoleAction = new EnterAndRunTestAssembly();
            break;

        case "4":
            testRunner5ConsoleAction = new RunForSelectedType();
            return;

        case "5":
            testRunner5ConsoleAction = new RunAllTestsSequentiallyXTimes();
            return;

        case "6":
            //testRunner5ConsoleAction = new RunForSelectedAssemblyXTimes();
            return;

        case "7":
            Console.WriteLine("Exit.");
            return;

        case "A":
            eventHandlings = new EventHandlingsSparseOutput();
			eventHandlings.Deinitialize();
			eventHandlings.Initialize(testRunner);
            Console.WriteLine($"Eventhandling \"{eventHandlings.Name}\" is set.");
			break;

        case "B":
            eventHandlings = new EventHandlingsExhaustiveOutput();
            eventHandlings.Deinitialize();
            eventHandlings.Initialize(testRunner);
            Console.WriteLine($"Eventhandling \"{eventHandlings.Name}\" is set.");
			break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

	testRunner5ConsoleAction?.Run(testRunner);


	Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

static ITestRunner5 InitializeTestRunner()
{
	// Your existing initialization logic here
	ITestRunner5 testRunner = new TestRunner5(); // Simplified for example
												 // Further initialization...
	return testRunner;
}
