using NextUnitTestRunner;
using NextUnitTestRunner.TestAttributes;

namespace TestRunnerTests
{
    public class TestRunnerTestsContainer
    {
        [Group("AttributeTest")]
        public void TestGroupAttribute()
        {

        }

        public const bool TestInjectDataAttribute_isEnabled = true;
        public const int TestInjectDataAttribute_count = 5;
        public const string TestInjectDataAttribute_message = "Hallo";
        /// <summary>
        /// This will test that data can be injected and is correctly contained.
        /// </summary>
        [InjectData(TestInjectDataAttribute_message, TestInjectDataAttribute_count, TestInjectDataAttribute_isEnabled)]
        public void TestInjectDataAttribute(string message, int count, bool isEnabled)
        {
            Assert.IsTrue(message == TestInjectDataAttribute_message);
            Assert.IsTrue(count == TestInjectDataAttribute_count);
            Assert.IsTrue(isEnabled == TestInjectDataAttribute_isEnabled);
        }

        /// <summary>
        /// 
        /// </summary>
        public void TestRandomAttributeOnce()
        {

        }

        /// <summary>
        /// This test will be executed multiple times.
        /// It's not allowed that ALL random values are the same. They have to be different.
        /// </summary>
        public void TestRandomAttributeMultiple()
        {

        }

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
    }
}
