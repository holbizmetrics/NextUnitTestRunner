﻿using NextUnit.Core;
using NextUnit.Core.Extensions;
using NextUnit.TestRunner;
using System.Diagnostics;
using NextUnit.Core.Asserts;
using NextUnit.TestEnvironment;
using NextUnit.TestRunner.TestRunners.NewFolder;

namespace NextUnit.Console.TestRunner.EventDisplays
{
    public class EventHandlingsSparseOutput : EventHandlings, IDisposable
    {
		public override void Initialize(ITestRunner5 testRunner5)
		{
			base.Initialize(testRunner5);
			testRunner5.TestExecuting += this.TestRunner_TestExecuting;
			testRunner5.ErrorEventHandler += this.TestRunner_ErrorEventHandler;
		}
		public override string Name => "Sparse Output";

		public void TestRunner_AfterTestRun(object? sender, ExecutionEventArgs e)
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

            Exception lastException = e.LastException;
            AssertException[] assertExceptions = lastException.ExtractExceptions<AssertException>();
            string asserts = assertExceptions.JoinExceptionTexts();

            string output = "MethodInfo: " + (e.TestResult.State == ExecutionState.NotStarted ? $"<White>{e.MethodInfo}</White>" : $"<Blue>{e.MethodInfo}</Blue>") +
        $@"
TestResult: {testResultStateText}
DisplayName: <Green>{e.TestResult.DisplayName}</Green>
Class: <Green>{e.TestResult.Class}, Namespace: {e.TestResult.Namespace}</Green>
Start: <Green>{e.TestResult.Start}</Green>
End: <Green>{e.TestResult.End}</Green>
Execution Time: <Green>{e.TestResult.ExecutionTime}</Green>
Workstation: <Green>{e.TestResult.Workstation}</Green>
";

            if (!string.IsNullOrEmpty(asserts))
            {
                output +=
$@"
--------
Asserts:
-----------

<Red>{string.Join(Environment.NewLine, assertExceptions.Select(ex => ex.Message))}</Red>

";
            }

            if (e.TestResult.State == ExecutionState.Failed)
            {
                string errorText =
            $@"<Red>{e.MethodInfo}</Red>
TestResult: <Green>{e.TestResult}</Green>";
                if (lastException != null)
                {
                    errorText =
            $@"{errorText}

Exception:

<Red>{e.LastException.GetAllMessagesIncludingAdditionalProperties()}</Red>";
                };
                errorText += Environment.NewLine;
                $"Test execution error: {errorText}".WriteColoredLine();
                output += errorText + Environment.NewLine + Environment.NewLine;
            }

            output.WriteColoredLine();
            System.Console.WriteLine(@"");
        }

        public void TestRunner_TestRunFinished(object sender, ExecutionEventArgs e)
        {
            // Show Hardware Snapshots
            System.Console.WriteLine("------------------------------");
            System.Console.WriteLine($"Finished - Hardware snapshot ({e.MethodInfo?.Name}) :");
            System.Console.WriteLine("------------------------------");

            string output = GetEnvironmentContext();
            output.WriteColoredLine();
            System.Console.WriteLine("");

            System.Console.WriteLine(NextUnitTestExecutionContext.ToString());
            System.Console.WriteLine("");
        }

        public void TestRunner_TestRunStarted(object sender, ExecutionEventArgs e)
        {
            // Show Hardware Snapshots
            System.Console.WriteLine("------------------------------");
            System.Console.WriteLine($"Started - Hardware snapshot: ({e.MethodInfo?.Name})");
            System.Console.WriteLine("------------------------------");
            string output = GetEnvironmentContext();
            output.WriteColoredLine();
            System.Console.WriteLine("");

            output = GetTestExecutionContext();
            output.WriteColoredLine();
            System.Console.WriteLine("");
        }

        public void TestRunner_ErrorEventHandler(object sender, ExecutionEventArgs e)
        {
            string errorText =
        $@"<Red>{e.MethodInfo}</Red>
TestResult: <Green>{e.TestResult}</Green>";
            if (e.LastException != null)
            {
                errorText =
        $@"{errorText}

Exception:

<Red>{e.LastException.GetAllMessagesIncludingAdditionalProperties()}</Red>";
            };
            errorText += Environment.NewLine;
            Trace.WriteLine("========================================================"); //.WriteColoredLine();
            Trace.WriteLine($"Test execution error ({e.MethodInfo.Name}): {errorText}"); //.WriteColoredLine();
            Trace.WriteLine("========================================================"); //.WriteColoredLine();
        }

        public void TestRunner_TestExecuting(object? sender, ExecutionEventArgs e)
        {
            $"Executing: {new StackFrame().GetMethod().Name}: {e.MethodInfo}".WriteColoredLine();
        }

        public void TestRunner_BeforeTestRun(object? sender, ExecutionEventArgs e)
        {
            $"Before Test Run: {new StackFrame().GetMethod().Name}: {e.MethodInfo}".WriteColoredLine();
        }

        public string GetEnvironmentContext()
        {
            // Format each ManagementObject and join them
            string biosInfo = string.Join(Environment.NewLine, NextUnitTestEnvironmentContext.BiosInfo.Select(bios => bios.GetText(System.Management.TextFormat.Mof)));

            string output =
        $@"MachineName: <Green>{NextUnitTestEnvironmentContext.MachineName}</Green>
CommandLine: <Green>{NextUnitTestEnvironmentContext.CommandLine}</Green>
ProcessorCount: <Green>{NextUnitTestEnvironmentContext.ProcessorCount}</Green>
BiosInfo: <Green>{biosInfo}</Green>
Capacity: <Green>{NextUnitTestEnvironmentContext.Capacity}</Green>
OperatingSystem: <Green>{NextUnitTestEnvironmentContext.OperatingSystem}</Green>";
            return output;
        }

        public string GetTestExecutionContext()
        {
            string output =
        $@"TestRunStart: <Green>{NextUnitTestExecutionContext.TestRunStart}</Green>;
TestRunEnd: <Green>{NextUnitTestExecutionContext.TestRunEnd}</Green>
TestRunTime: <Green>{NextUnitTestExecutionContext.TestRunTime}</Green>";
            return output;
        }

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Deinitialize();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}
		public void Deinitialize()
		{
            if (_testRunner5 == null) return;
			_testRunner5.TestExecuting -= this.TestRunner_TestExecuting;
			_testRunner5.ErrorEventHandler -= this.TestRunner_ErrorEventHandler;
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~EventHandlingsSparseOutput()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}