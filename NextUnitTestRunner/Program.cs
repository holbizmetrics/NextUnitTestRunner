// See https://aka.ms/new-console-template for more information
using NextUnitTestRunner;
using System.Diagnostics;

Trace.Listeners.Add(new ConsoleTraceListener());
TestRunner testRunner = new TestRunner();
testRunner.Run(typeof(TestClass));

Trace.WriteLine("Test run finished.");