using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnit.CodeCoverage.Tests
{
    /// <summary>
    /// This will contain code coverage tests.
    /// </summary>
    public class CodeCoverageTests
    {
        [Test]
        [Group(nameof(CodeCoverage))]
        public void CodeCoverageTest()
        {
            Debugger.Launch();
        }
    }
}
