using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnit.TestRunner.TestClasses
{
    public class AnotherTestClass
    {
        [Test]
        public void AnotherTestClassToSeeItWillBeExecutedAsWellTest()
        {
            Trace.WriteLine($"We're running here: {this.GetType().FullName}");
        }
    }
}
