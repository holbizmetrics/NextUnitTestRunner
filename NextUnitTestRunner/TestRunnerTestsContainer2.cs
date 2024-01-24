﻿using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestRunnerTests
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
            //Assert.IsTrue();
        }
        #endregion Asserts Tests

        #region ConditionalRetryAttribute Tests
        private static int _externalServiceState = 0;

        private static bool IsServiceInDesiredState()
        {
            return _externalServiceState == 5;
        }

        [Test]
        [ConditionalRetry(nameof(IsServiceInDesiredState), maxRetry: 10)]
        public void TestExternalServiceInteraction()
        {
            _externalServiceState++;
            Console.WriteLine($"Attempt {_externalServiceState}: Testing interaction with the external service");
        }

        [Test]
        [ConditionalRetry(nameof(IsServiceInDesiredState), 1)]
        public void ConditionalRetryAttributeTest()
        {

        }

        public bool Condition { get; set; } = false;

        #endregion ConditionalRetryAttribute Tests

        #region ConditionAttribute Tests
        [Test]
        [Condition(true, nameof(Blub))]
        public void ConditionAttributeTest()
        {
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
        }
        #endregion CustomExtendableAttribute Tests

        [Test]
        public void Blub()
        {

        }

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

        #region ExtendedTestAttribute Tests
        [ExtendedTest]
        public void ExtendedTestAttributeTest()
        { 
        }
        #endregion ExtendedTestAttribute Tests

        #region FuzzingAttribute Tests
        [Test]
        [Fuzzing]
        public void FuzzingAttributeTest(int n1, int n2)
        {
            int result = n1 + n2;
        }
        #endregion

        #region Group Attribute Tests
        [Test]
        [Group("AttributeTest")]
        public void GroupAttributeTest()
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
        [Random(-1000, 2000)]
        public void TestRandomAttributeOnce(int param1)
        {

        }

        [Test]
        [Random(1, 2)]
        [Random(5, 2, 1)] //should fail, because 5 > 2, min and then max has to be specified. Not vice versa.
        [Random(1, 1, 1)]
        public void TestSeveralRandomAttributesEachOnlyExecutedOnce(int param1)
        {

        }

        [Test]
        public void TestSeveralRandomAttributesEachSeveralTimes(int param1)
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
        public void TimeoutAttributeSuccedsBecauseTestIsExecutedInTime()
        {
            //Do something
            Thread.Sleep(500);
        }
        #endregion Timeout Attribute Tests

    }
}