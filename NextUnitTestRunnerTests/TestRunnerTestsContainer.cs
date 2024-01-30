using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner;
using System.Diagnostics;

namespace NextUnit.TestRunnerTests
{





    public class TestRunnerTestsContainer
    {
        public TestRunnerTestsContainer()
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

        #region ConditionalRetryAttribute Tests
        #endregion ConditionalRetryAttribute Tests

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
        public void MyTestMethod(IMyService service)
        {
            // Assert that the service is successfully injected
            Assert.IsNotNull(service);

            // Use the service in your test
            var data = service.GetData();
            Assert.AreEqual("Sample Data", data);

            // Additional test logic...
        }
        #endregion DependencyInjectionAttribute Tests

        #region Group Attribute Tests
        [Test]
        [Group("AttributeTest")]
        public void TestGroupAttribute()
        {

        }
        #endregion Group Attribute Tests

        #region InjectDataAttribute Tests
        public const bool TestInjectDataAttribute_isEnabled = true;
        public const int TestInjectDataAttribute_count = 5;
        public const string TestInjectDataAttribute_message = "Hallo";
        /// <summary>
        /// This will test that data can be injected and is correctly contained.
        /// </summary>
        [Test]
        [InjectData(TestInjectDataAttribute_message, TestInjectDataAttribute_count, TestInjectDataAttribute_isEnabled)]
        [InjectData("Crazy. It works!", 7, false)]
        [InjectData("This as well!", 3, false)]
        public void TestInjectDataAttribute(string message, int count, bool isEnabled)
        {
            Assert.IsTrue(message == TestInjectDataAttribute_message);
            Assert.IsTrue(count == TestInjectDataAttribute_count);
            Assert.IsTrue(isEnabled == TestInjectDataAttribute_isEnabled);
        }
        #endregion InjectDataAttribute Tests

        #region Random Attribute Tests

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Random(1, 2)]
        public void TestRandomAttributeOnce()
        {

        }

        [Test]
        [Random(1,2)]
        [Random(5, 2, 1)]
        [Random(1,1, 1)]
        public void TestSeveralRandomAttributesEachOnlyExecutedOnce()
        {

        }

        [Test]
        public void TestSeveralRandomAttributesEachSeveralTimes()
        {

        }

        /// <summary>
        /// Mixed Random attributes, but all are specified correctly.
        /// </summary>
        [Test]
        [Random(1,3,2)]
        public void TestSeveralRandomLegalAttributesMixed()
        {

        }

        /// <summary>
        /// Testing with specifications that either shouldn't be allowed or make no sense.
        /// </summary>
        [Test]
        [Random(0, 0, 0)]   //it wouldn't make sense to execute 0 times. As well as max = min.
        [Random(0, 0, -1)]  //it wouldn't make sense to execute -1 times. As well as max = min.
        [Random(1,5, -1)]   //it wouldn't make sense to execute -1 times. Though the intervals are ok.
        public void TestSeveralRandomAttributesMixedInvalid()
        {

        }

        /// <summary>
        /// This test will be executed multiple times.
        /// It's not allowed that ALL random values are the same. They have to be different.
        /// </summary>
        [Test]
        [Random(-200, 200, 5)]
        public void TestRandomAttributeMultiple(int min, int max)
        {

        }
        #endregion random attributes tests

        #region RunAfterAttribute Tests
        [Test]
        [RunAfter("")]
        public void RunAfterAttributeTest()
        {

        }
        #endregion RunAfterAttribute Tests

        #region RunBeforeAttribute Tests
        [Test]
        [RunBefore("")]
        public void RunBeforeAttributeTest()
        {

        }
        #endregion RunBeforeAttribute Tests     

        #region RunAllDelegatePermutations Tests
        [RunAllDelegatePermutations("Test1", "Test2", "Test3")]
        [Test]
        public static void RunAllDelegatePermutations()
        {
            
        }

        public static void Test1()
        {

        }

        public static void Test2()
        {

        }
        
        public static void Test3()
        {

        }
        #endregion RunAllDelegatePermutations Tests

        #region SeveralDataAttributesTests
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [RunAfter("")]
        [InjectDataAttribute]
        public void RunAfterInjectDataAttributesTest()
        {
        }

        [Test]
        [RunBefore("")]
        [InjectDataAttribute]
        public void RunBeforeInjectDataAttributesTest()
        {
        }

        [Test]
        [RunBefore("")]
        [RunAfter("")]
        [InjectData]
        public void RunBeforeRunAfterInjectDataAttributesTest()
        {

        }
        #endregion SeveralDataAttributesTests

        #region Timeout Attribute Tests
        [Test]
        [Timeout(3)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeTestFailsBecauseTestNeedsTooLong()
        {
            //Do something
            Thread.Sleep(500);
        }

        [Test]
        [Timeout(30000)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeSuccedsBecauseTestIsExecutedInTime()
        {
            //Do something
            Thread.Sleep(500);
        }
        #endregion Timeout Attribute Tests

    }
}
