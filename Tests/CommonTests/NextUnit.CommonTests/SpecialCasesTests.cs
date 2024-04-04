using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnit.CommonTests
{
    /// <summary>
    /// If those tests works, this guarantees a lot more integrity of test detection.
    /// Because in the early beginning detecting of static tests was not supported.
    /// </summary>
    public class SpecialCasesTests
    {
        public static List<string> ExecutedTests = new List<string>();

        [Test]
        public static void StaticTestToDetect()
        {
            AddIfNotExists(FunctionName());
        }

        [Test]
        public async static void StaticAsyncTestToDetect()
        {
            AddIfNotExists(FunctionName());
        }

        [Test]
        public async void AsyncVoidTest()
        {
            AddIfNotExists(FunctionName());
        }

        private static string FunctionName()
        {
            return new StackFrame(1).GetMethod().Name;
        }

        private static void AddIfNotExists(string name)
        {
            if (!ExecutedTests.Contains(name))
            {
                ExecutedTests.Add(name);
            }
        }
    }
}