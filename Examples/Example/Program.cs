// See https://aka.ms/new-console-template for more information

using Blub.AdditionallyNeeded;
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestRunners.NewFolder;

Console.WriteLine("This is an example to use the testrunner yourself, if you want to use all the extension points!");

// initialize
ITestRunner5 testRunner5 = new TestRunner5();
testRunner5.TestDiscoverer = new ExampleTestDiscoverer();
testRunner5.Combinator = new ExampleCombinator();
testRunner5.InstanceCreationBehavior = new ExampleInstanceCreationBehavior();
testRunner5 = testRunner5.With(new AutofixtureAutomoqAttributeAttributeLogicMapper());

testRunner5.TestExecuting += TestRunner5_TestExecuting;
testRunner5.AfterTestRun += TestRunner5_AfterTestRun;

// and run
testRunner5.Run(typeof(NestedClassToRunTestsFor));

void TestRunner5_AfterTestRun(object sender, NextUnit.TestRunner.ExecutionEventArgs e)
{
    Console.WriteLine($"Executing test: {e.MethodInfo.Name} {e.TestResult}");
}

void TestRunner5_TestExecuting(object sender, NextUnit.TestRunner.ExecutionEventArgs e)
{
}



/// <summary>
/// The TestRunner above will cause all correctly defined tests to run in here.
/// </summary>
public class NestedClassToRunTestsFor
{
    [Test]
    public void Test()
    {

    }

    [Test]
    [InjectData(5, 8)]
    public static void AsyncMethodTest(int param1, int param2)
    {
        Assert.AreEqual(5, param1);
        Assert.AreEqual(8, param2);
    }
}