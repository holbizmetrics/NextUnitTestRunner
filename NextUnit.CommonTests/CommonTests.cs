using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using static NextUnit.Core.AttributeLogic.LogicHandlers.CompileAttributeLogicHandler;
using System.Reflection;
using NextUnit.TestRunner.Attributes;
using NextUnit.Core.Extensions;

namespace NextUnit.CommonTests
{
    /// <summary>
    /// The common tests will be tests that contain different things.
    /// It won't contain things of testing the framework necessarily
    /// in general. BUT other things you could do using the framework.
    /// 
    /// For example: Trying other types of tests and simulating a big test class.
    /// </summary>
    public class Tests
    {
        [Test]
        [Group(nameof(CommonTests))]
        public void Setup()
        {
        }

        [Group(nameof(CommonTests))]
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Group(nameof(CommonTests))]
        [Test]
        public void Test2()
        {

        }

        #region Asserts Tests
        [Test]
        [Group("Asserts")]
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
        [Group(nameof(AllCombinationsAttribute))]
        public void AllCombinationsAttributeTest(
            [Values(1, 2, 3)] int x,
            [Values("A", "B")] string s)
        {
            // Test code here...
            Trace.WriteLine($"x: {x}, s: {s}");
        }

        [Test, AllCombinations(
            conditionMethodName: nameof(MyConditionMethod),
            strategy: PermutationStrategy.Pairwise
        )]
        [Group(nameof(AllCombinationsAttribute))]
        public void AllCombinationsAttributePairwiseTest(
            [Values(1, 2, 3)] int x,
            [Values("A", "B")] string s)
        {
            // Test code here...
            Trace.WriteLine($"x: {x}, s: {s}");
        }
        #endregion AllCombinationsAttribute Tests

        #region CompileAttribute Tests

        public const string source =
@"
using System;
namespace DynamicNamespace
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}";
        [Group(nameof(CompileAttribute))]
        [Compile(source: source, useFile: false, methodName: "DynamicMethod")]
        public void TestCompiledCode()
        {
            var compiledObject = CompiledObjectRegistry.Retrieve(MethodBase.GetCurrentMethod().Name);
            var methodInfo = compiledObject.GetType().GetMethod("Add");
            var result = methodInfo.Invoke(compiledObject, new object[] { 1, 2 });

            // Assert on the `result` as needed
        }
        #endregion CompileAttribute Tests


        #region ConditionAttribute Tests
        /// <summary>
        /// Needed for the ConditionAttributeTest below.
        /// </summary>
        public bool Blub()
        {
            return DateTime.Now > DateTime.Now; //this will never be true for sure.
        }

        [Test]
        [Group(nameof(ConditionAttribute))]
        [Condition(nameof(Blub))]
        public void ConditionAttributeTest()
        {
        }
        #endregion ConditionAttribute Tests

        #region ConditionalRetryAttribute Tests
        private static int _externalServiceState = 0;
        private static bool IsServiceInDesiredState()
        {
            return _externalServiceState == 5;
        }

        [Test]
        [Group(nameof(ConditionalRetryAttribute))]
        [ConditionalRetry(nameof(IsServiceInDesiredState), maxRetry: 10)]
        public void TestExternalServiceInteraction()
        {
            _externalServiceState++;
            Trace.WriteLine($"Attempt {_externalServiceState}: Testing interaction with the external service");
        }

        [Test]
        [Group(nameof(ConditionalRetryAttribute))]
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
        [Group(nameof(ConditionAttribute))]
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
        [Group(nameof(CustomExtendableAttribute))]
        public void CustomExtendableAttributeTest()
        {
            //Writes a text to the console for now.
            //How? The text is specified in the console.
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
        [Group(nameof(DependencyInjectionAttribute))]
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
        [Group(nameof(TestGroupAttribute))]
        public void TestGroupAttribute()
        {
            IEnumerable<Attribute> attributes = new StackFrame().GetMethod().GetCustomAttributes();
            IEnumerable<GroupAttribute> groupAttributes = attributes.OfType<GroupAttribute>().ToArray();
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
        [Group(nameof(InjectDataAttribute))]
        [InjectData(TestInjectDataAttribute_message, TestInjectDataAttribute_count, TestInjectDataAttribute_isEnabled)]
        [InjectData("Crazy. It works!", 7, false)]
        [InjectData("This as well!", 3, false)]
        public void TestInjectDataAttribute(string message, int count, bool isEnabled)
        {
            Assert.IsTrue(message == TestInjectDataAttribute_message);
            Assert.IsTrue(count == TestInjectDataAttribute_count);
            Assert.IsTrue(isEnabled == TestInjectDataAttribute_isEnabled);
        }

        [Test]
        [Group(nameof(InjectDataAttribute))]
        [InjectData(1, 2, 3, "Name", false)]
        public void InjectDataAttributeTest(int intParam1, int intParam2, int intParam3, string name, bool @switch)
        {
            TestParameters(new StackFrame(), Assert.IsNotNull);
        }

        public delegate void IsNotNull(object o, string message);
        public static void TestParameters(StackFrame stackFrame, params IsNotNull[] blub)
        {
            //MethodInfo methodInfo = stackFrame.GetMethod() as MethodInfo;
            //ParameterInfo[] parameterInfo = methodInfo.GetParameters();
            //foreach(ParameterInfo pi in parameterInfo)
            //{
            //    var frame = new StackTrace(true).GetFrame(1);
            //    var arguments = ((MethodInfo)frame.GetMethod()).GetParameters();
            //    int argumentIndex = pi.Position;
            //    object value = argumentIndex[argumentIndex];
            //}
        }

        #endregion InjectDataAttribute Tests

        #region PermutationAttribute Test
        [Test]
        [Group(nameof(PermutationAttribute))]
        [Permutation]
        public void PermutationAttributeTest()
        {

        }
        #endregion PermutationAttribute Test

        #region Random Attribute Tests

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Group(nameof(RandomAttribute))]
        [Random(1, 2)]
        public void TestRandomAttributeOnce()
        {

        }

        [Test]
        [Group(nameof(RandomAttribute))]
        [Random(1, 2)]
        [Random(5, 2, 1)]
        [Random(1, 1, 1)]
        public void TestSeveralRandomAttributesEachOnlyExecutedOnce()
        {

        }

        [Test]
        [Group(nameof(RandomAttribute))]
        public void TestSeveralRandomAttributesEachSeveralTimes()
        {

        }

        /// <summary>
        /// Mixed Random attributes, but all are specified correctly.
        /// </summary>
        [Test]
        [Group(nameof(RandomAttribute))]
        [Random(1, 3, 2)]
        public void TestSeveralRandomLegalAttributesMixed()
        {

        }

        /// <summary>
        /// Testing with specifications that either shouldn't be allowed or make no sense.
        /// </summary>
        [Test]
        [Group(nameof(RandomAttribute))]
        [Random(0, 0, 0)]   //it wouldn't make sense to execute 0 times. As well as max = min.
        [Random(0, 0, -1)]  //it wouldn't make sense to execute -1 times. As well as max = min.
        [Random(1, 5, -1)]   //it wouldn't make sense to execute -1 times. Though the intervals are ok.
        public void TestSeveralRandomAttributesMixedInvalid()
        {

        }

        /// <summary>
        /// This test will be executed multiple times.
        /// It's not allowed that ALL random values are the same. They have to be different.
        /// </summary>
        [Test]
        [Group(nameof(RandomAttribute))]
        [Random(-200, 200, 5)]
        public void TestRandomAttributeMultiple(int min, int max)
        {
        }
        #endregion random attributes tests

        #region RunAfterAttribute Tests
        [Test]
        [Group(nameof(RunAfterAttribute))]
        [RunAfter("")]
        public void RunAfterAttributeTest()
        {

        }
        #endregion RunAfterAttribute Tests

        #region RunBeforeAttribute Tests
        [Test]
        [Group(nameof(RunBeforeAttribute))]
        [RunBefore("")]
        public void RunBeforeAttributeTest()
        {

        }
        #endregion RunBeforeAttribute Tests     
        #region RunAllDelegatePermutations Tests
        [RunAllDelegatePermutationsAttribute("Test_1", "Test_2", "Test_3")]
        [Group(nameof(RunAllDelegatePermutationsTest))]
        [Test]
        public static void RunAllDelegatePermutationsTest()
        {

        }

        public static void Test_1()
        {

        }

        public static void Test_2()
        {

        }

        public static void Test_3()
        {

        }
        #endregion RunAllDelegatePermutations Tests
        #region SeveralDataAttributesTests
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Group("SeveralAttributesApplied")]
        [RunAfter("")]
        [InjectDataAttribute]
        public void RunAfterInjectDataAttributesTest()
        {
        }

        [Test]
        [Group("SeveralAttributesApplied")]
        [RunBefore("")]
        [InjectDataAttribute]
        public void RunBeforeInjectDataAttributesTest()
        {
        }

        [Test]
        [Group("SeveralAttributesApplied")]
        [RunBefore("")]
        [RunAfter("")]
        [InjectData]
        public void RunBeforeRunAfterInjectDataAttributesTest()
        {

        }
        #endregion SeveralDataAttributesTests
        #region Timeout Attribute Tests
        [Test]
        [Group(nameof(TimeoutAttribute))]
        [Timeout(3)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeTestFailsBecauseTestNeedsTooLong()
        {
            //Do something
            Thread.Sleep(500);
        }

        [Test]
        [Group(nameof(TimeoutAttribute))]
        [Timeout(30000)] //will make a test fail if it takes longer to execute then specified t timeout in attribute.
        public void TimeoutAttributeSuccedsBecauseTestIsExecutedInTime()
        {
            //Do something
            Thread.Sleep(500);
        }
        #endregion Timeout Attribute Tests

        [Test]
        [Group("Blub")]
        [Group("Test")]
        public void BlubTest()
        {
            Assert.Fail();
        }
    }
}