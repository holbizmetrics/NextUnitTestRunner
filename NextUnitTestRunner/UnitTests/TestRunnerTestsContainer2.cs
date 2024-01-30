using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.NextUnit;
using Moq;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.Attributes;
using System.Diagnostics;
using System.Reflection;

namespace NextUnit.TestRunner.UnitTests
{
    public class TestRunnerTestsContainer2
    {
        public TestRunnerTestsContainer2()
        {
        }

        #region Asserts Tests
        [Test]
        public void SeveralAssertsTest()
        {
            Trace.WriteLine(new StackFrame(1).GetMethod().Name);
            //Assert.IsTrue();
        }
        #endregion Asserts Tests

        #region AllCombinationsAttribute Tests
        private bool MyConditionMethod(object[] combination)
        {
            // Your condition logic here...
            return combination[0] is int x && x > 0;
        }

        [Test, AllCombinations(
            conditionMethodName: nameof(MyConditionMethod),
            strategy: PermutationStrategy.Pairwise
        )]
        public void AllCombinationsAttributeTest(
            [Values(1, 2, 3)] int x,
            [Values("A", "B")] string s)
        {
            // Test code here...
            Trace.WriteLine("x: {x}, s: {s}");
        }

        [Test, AllCombinations(
            conditionMethodName: nameof(MyConditionMethod),
            strategy: PermutationStrategy.Pairwise
        )]
        public void AllCombinationsAttributePairwiseTest(
            [Values(1, 2, 3)] int x,
            [Values("A", "B")] string s)
        {
            // Test code here...
            Trace.WriteLine("x: {x}, s: {s}");
        }
        #endregion AllCombinationsAttribute Tests

        private static bool IsServiceInDesiredState()
        {
            return _externalServiceState == 5;
        }

        #region ConditionAttribute Tests
        /// <summary>
        /// Needed for the ConditionAttributeTest below.
        /// </summary>
        public bool Blub()
        {
            return DateTime.Now > DateTime.Now; //this will never be true for sure.
        }

        [Test]
        [Condition(nameof(Blub))]
        public void ConditionAttributeTest()
        {
        }
        #endregion ConditionAttribute Tests

        #region ConditionalRetryAttribute Tests
        private static int _externalServiceState = 0;

        [Test]
        [ConditionalRetry(nameof(IsServiceInDesiredState), maxRetry: 10)]
        public void TestExternalServiceInteraction()
        {
            _externalServiceState++;
            Trace.WriteLine($"Attempt {_externalServiceState}: Testing interaction with the external service");
        }

        [Test]
        [ConditionalRetry(nameof(IsServiceInDesiredState), 1)]
        public void ConditionalRetryAttributeTest()
        {

        }
        #endregion ConditionalRetryAttribute Tests

        #region ConditionAttribute Tests
        private bool IsConditionMet()
        {
            // Define your condition logic here
            return true; // Example condition
        }

        [Test]
        [Condition(nameof(IsConditionMet))]
        public void ConditionalTest()
        {
            // Test logic here...
        }
        #endregion ConditionAttribute Tests

        #region CustomExtendableAttribute Tests
        /// <summary>
        /// This provides extendable attributes to extend the framework.
        /// Specifics have not been implemented, yet. But for example could already be used to write
        /// messages to console, debug, etc. 
        /// </summary>
        [Test]
        [ConsoleCustomExtendable]
        public void CustomExtendableAttributeTest()
        {
            //Writes a text to the console for now.
            //How? The text is specified in the console.
        }

        public class RangeDataAttribute : CustomExtendableAttribute
        {
            private readonly int start;
            private readonly int end;

            public RangeDataAttribute(int start, int end)
            {
                this.start = start;
                this.end = end;
            }

            public override IEnumerable<object> GetData(MethodInfo methodInfo)
            {
                return Enumerable.Range(start, end - start + 1).Cast<object>();
            }
        }

        /// <summary>
        /// Will execute the method from Range Start to Range End.
        /// </summary>
        /// <param name="value"></param>
        [Test]
        [RangeData(1, 5)]
        public void CustomExtendableAttributeRangeDataTest(int value)
        {

        }

        #endregion CustomExtendableAttribute Tests

        #region DependencyInjectionAttribute Tests
        public interface IMyService
        {
            string GetData();
        }

        public class MyServiceImplementation : IMyService
        {
            public string GetData()
            {
                return "Sample Data";
            }
        }

        [Test, DependencyInjection(typeof(IMyService))]
        public void DependencyInjectionAttributeTest(IMyService service)
        {
            // Assert that the service is successfully injected
            Assert.IsNotNull(service);

            // Use the service in your test
            var data = service.GetData();
            Assert.AreEqual("Sample Data", data);

            // Additional test logic...
        }
        #endregion DependencyInjectionAttribute Tests

        #region DontRunDuringAttribute Tests
        [Test]
        [DontRunDuring("2024-05-01T00:00:00", "2024-05-01T00:00:00")]
        public void DontRunDuringAttributeTest()
        {

        }
        #endregion DontRunDuringAttribute Tests

        #region ExecuteUntilTimeoutAttribute Test
        [Test]
        //[ExecuteUntilTimeout("1", "1")] //use string instead
        public void ExecuteUntilTimeoutAttributeTest()
        {
        }
        #endregion ExecuteUntilTimeoutAttribute Test

        //#region ExtendedTestAttribute Tests
        //[ExtendedTest]
        //public void ExtendedTestAttributeTest()
        //{ 
        //}
        //#endregion ExtendedTestAttribute Tests

        #region FuzzingAttribute Tests
        [Test]
        [Fuzzing]
        public void FuzzingAttributeTest(int n1, int n2)
        {
            int result = n1 + n2;
            Console.WriteLine($"n1 + n2: {n1 + n2}");
        }
        #endregion

        #region Group Attribute Tests
        [Test]
        [Group("AttributeTest")]
        public void GroupAttributeTest()
        {

        }
        #endregion Group Attribute Tests

        #region Random Attribute Tests

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Random(-1000, 2000)] //if no count for the execution count is specified this will automatically be set to 1.
        public void TestRandomAttributeOnce(int param1)
        {

        }

        [Test]
        [Random(1, 2)]
        [Random(1, 1, 1)]
        public void TestSeveralRandomAttributeEachOnlyExecutedOnce(int param1)
        {

        }

        [Test]
        [Random(1, 1, 0)]   //should do nothing, because count should be > 0 to make sense. Probably generate error?
        [Random(-1, 1, -3)]  //should do either nothing right now, only count >0 makes sense. Probably generate an error?
        public void TestSeveralRandomAttributesEachOnlyExecutedInvalidCount()
        {

        }

        [Test]
        [Random(5, 2)]      //should fail because 5 > 2 -> set min first, then max.
        public void TestRandomAttributFailsBecauseMinGreaterMax(int param1)
        {

        }

        /// <summary>
        /// Mixed Random attributes, but all are specified correctly.
        /// </summary>
        [Test]
        [Random(1, 3, 2)]
        public void TestSeveralRandomLegalAttributesMixed(int param1)
        {

        }

        /// <summary>
        /// Testing with specifications that either shouldn't be allowed or make no sense.
        /// </summary>
        [Test]
        [Random(0, 0, 0)]   //it wouldn't make sense to execute 0 times. As well as max = min.
        [Random(0, 0, -1)]  //it wouldn't make sense to execute -1 times. As well as max = min.
        [Random(1, 5, -1)]   //it wouldn't make sense to execute -1 times. Though the intervals are ok.
        public void TestSeveralRandomAttributesMixedInvalid(int param1)
        {

        }

        /// <summary>
        /// This test will be executed multiple times.
        /// It's not allowed that ALL random values are the same. They have to be different.
        /// </summary>
        [Test]
        [Random(-200, 200, 5)]
        public void TestRandomAttributeMultiple(int param1)
        {

        }
        #endregion random attributes tests

        #region RunAfterAttribute Tests
        [Test]
        [RunAfter("2024-05-01T00:00:00")]
        public void RunAfterAttributeTest()
        {

        }
        #endregion RunAfterAttribute Tests

        #region RunAllDelegatePermutations Tests
        [Test]
        [RunAllDelegatePermutations("PermutationTest1", "PermutationTest2", "PermutationTest3")]
        public void RunAllDelegatePermutationsTest()
        {

        }

        public static void PermutationTest1()
        {
            Trace.WriteLine("PermutationTest: 1");
        }

        public static void PermutationTest2()
        {
            Trace.WriteLine("PermutationTest: 2");
        }

        public static void PermutationTest3()
        {
            Trace.WriteLine("PermutationTest: 3");
        }
        #endregion RunAllDelegatePermutations Tests

        #region RunBeforeAttribute Tests
        [Test]
        [RunBefore("2024-05-01T00:00:00")]
        public void RunBeforeAttributeTest()
        {

        }
        #endregion RunBeforeAttribute Tests

        #region RunDuringAttribute Tests
        [Test]
        [DontRunDuring("2024-05-01T00:00:00", "2024-05-01T00:00:00")]
        public void RunDuringAttributeTest()
        {

        }
        #endregion RunDuringAttribute Tests

        #region SkipAttribute Tests

        [Test]
        [Skip]
        public void SkipAttributeTest()
        {

        }
        #endregion SkipAttribute Tests

        #region TimeoutAttribute Tests
        [Test]
        [Timeout(3)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeTestFailsBecauseTestNeedsTooLong()
        {
            //Do something
            Thread.Sleep(500);
        }

        [Test]
        [Timeout(30000)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeSucceedsBecauseTestIsExecutedInTime()
        {
            //Do something
            Thread.Sleep(500);
        }
        #endregion Timeout Attribute Tests

        #region combined attributes tests
        //in this region we'll test several different attributes at once.

        //For the moment either all of them or a lot may still fail. BUT those tests are here because in the long run this has to work as well.

        #endregion combined attributes tests

        #region AutoFixture.AutoMoq Test
        [InlineAutoData]
        public void InlineAutoDataTest()
        {

        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
                : this(null)
            {
            }

            protected AutoMoqDataAttribute(Action<IFixture>? cfg)
                : base(() =>
                {
                    var fixture = new Fixture();
                    fixture.Customize(new AutoMoqCustomization
                    {
                        ConfigureMembers = true,
                        GenerateDelegates = true,
                    });
                    cfg?.Invoke(fixture);
                    return fixture;
                })
            {
            }
        }

        public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
        {
            public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects)
            {
            }
        }

        public interface ISomeInterface
        {

        }

        public class MySut
        {

        }

        [InlineAutoMoqData(3, 4)]
        [InlineAutoMoqData(33, 44)]
        [InlineAutoMoqData(13, 14)]
        public void SomeUnitTest(int DataFrom, int OtherData, [Frozen] Mock<ISomeInterface> theInterface, MySut sut)
        {
        }
        #endregion AutoFixture.AutoMoq Test
        ~TestRunnerTestsContainer2()
        {

        }
    }
}
