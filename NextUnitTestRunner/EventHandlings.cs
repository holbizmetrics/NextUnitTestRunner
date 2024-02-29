using NextUnit.Core;
using NextUnit.TestRunner.Extensions;
using NextUnit.TestRunner;


namespace NextUnit.Console.TestRunner
{
    public class EventHandlings
    {
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
            output.WriteColoredLine();
            System.Console.WriteLine(@"");
        }

        public void TestRunner_TestRunFinished(object sender, ExecutionEventArgs e)
        {
            // Show Hardware Snapshots
            System.Console.WriteLine("------------------");
            System.Console.WriteLine($"Finished - Hardware snapshot ({e.MethodInfo?.Name}) :");
            System.Console.WriteLine("------------------");

            string output = GetEnvironmentContext();
            output.WriteColoredLine();
            System.Console.WriteLine("");

            System.Console.WriteLine(NextUnitTestExecutionContext.ToString());
            System.Console.WriteLine("");
        }

        public void TestRunner_TestRunStarted(object sender, ExecutionEventArgs e)
        {
            // Show Hardware Snapshots
            System.Console.WriteLine("------------------");
            System.Console.WriteLine($"Started - Hardware snapshot: ({e.MethodInfo?.Name})");
            System.Console.WriteLine("------------------");
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

<Red>{e.LastException}</Red>";
            };
            errorText += Environment.NewLine;
            $"Test execution error: {errorText}".WriteColoredLine();
        }



        public void TestRunner_TestExecuting(object? sender, ExecutionEventArgs e)
        {
        }

        public void TestRunner_BeforeTestRun(object? sender, ExecutionEventArgs e)
        {
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
    }
}
