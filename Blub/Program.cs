// See https://aka.ms/new-console-template for more information
using Blub.AdditionallyNeeded;
using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestRunners.NewFolder;

Console.WriteLine("Hello, World!");

ITestRunner5 testRunner5 = new TestRunner5();
testRunner5.TestDiscoverer = new BlubTestDiscoverer();
testRunner5.Combinator = new BlubCombinator();
testRunner5.InstanceCreationBehavior = new BlubInstanceCreationBehavior();
testRunner5 = testRunner5.With(new AutofixtureAutomoqAttributeAttributeLogicMapper());
testRunner5.Run(typeof(NestedClassToRunTestsFor));

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