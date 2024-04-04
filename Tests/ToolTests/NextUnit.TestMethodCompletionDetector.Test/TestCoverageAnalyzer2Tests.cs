using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.TestMethodCompletionDetector.NewFolder1;

namespace NextUnit.TestMethodCompletionDetectorTest
{
    public class TestCoverageAnalyzer2Tests
    {
        [Test]
        public void TestCoverageAnalyzerTest()
        {
            string sourceCode =
@"public class MyClass
{
  public void PublicMethod1() { }
  private void PrivateMethod2() { }
}";

            string testCode =
        @"public class MyTest
{
  [Test]
  public void TestPublicMethod()
  {
    new MyClass().PublicMethod1();
  }
}";
            TestCoverageAnalyzer2 testCoverageAnalyzer2 = new TestCoverageAnalyzer2();
            var testCoverageResult = testCoverageAnalyzer2.Analyze(sourceCode, testCode);
            Console.WriteLine(testCoverageResult.ToString());
            //Assert.AreEqual(2, testCoverageResult.TotalMethodsCount);
            //Assert.AreEqual(100.0d, testCoverageResult.TestedPercentage);
            //Assert.AreEqual(0, testCoverageResult.UntestedMethods);
        }
    }
}
