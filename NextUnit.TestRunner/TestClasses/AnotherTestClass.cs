using NextUnit.Core.TestAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
