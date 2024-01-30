using NextUnit.Core.Extensions;
using System.Reflection;

namespace NextUnit.Core.AttributeLogic.LogicHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class FuzzingAttributeLogicHandler : IAttributeLogicHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="testMethod"></param>
        /// <param name="testInstance"></param>
        public void ProcessAttribute(Attribute attribute, MethodInfo testMethod, object testInstance)
        {
            var testDataGenerator = new TestDataGenerator();
            var parameters = testMethod.GetParameters();

            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType.IsComparable() && parameter.ParameterType.IsEquatable())
                {
                    var testData = testDataGenerator.GenerateTestData(parameter.ParameterType);

                    foreach (var data in testData)
                    {
                        testMethod.Invoke(testInstance, data);
                        // Handle and record test results
                    }
                }
            }
        }

        public class TestDataGenerator
        {
            private Random _random = new Random();

            public IEnumerable<object[]> GenerateTestData(Type parameterType)
            {
                List<object[]> testData = new List<object[]>();

                // Example for int type
                if (parameterType == typeof(int))
                {
                    testData.Add(new object[] { _random.Next() }); // Random int
                    testData.Add(new object[] { int.MaxValue });   // Max value
                    testData.Add(new object[] { int.MinValue });   // Min value
                }

                // Example for string type
                else if (parameterType == typeof(string))
                {
                    testData.Add(new object[] { Guid.NewGuid().ToString() });  // Random string
                    testData.Add(new object[] { string.Empty });               // Empty string
                    testData.Add(new object[] { null });                       // Null
                }

                // Add cases for other types as needed...

                return testData;
            }
        }
    }
}
