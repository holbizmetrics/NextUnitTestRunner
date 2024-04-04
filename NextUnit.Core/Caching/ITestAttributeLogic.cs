using System.Reflection;

namespace NextUnit.Core.Caching
{
    public interface ITestAttributeLogic
    {
        void BeforeMethodExecution(MethodInfo testMethod, object testInstance);
        void AfterMethodExecution(MethodInfo testMethod, object testInstance, TestResult testResult);
        bool ShouldExecute(MethodInfo testMethod, object testInstance);
        IEnumerable<object[]> GetExecutionParameters(MethodInfo testMethod);
    }

    public class TestAttributeLogic
    {
        Dictionary<MethodInfo, ITestAttributeLogic> attributeLogicCache = new Dictionary<MethodInfo, ITestAttributeLogic>();
        public void Test()
        {
            //foreach(var testMethod in discoveredTestMethods)
            //{
            //    attributeLogicCache[testMethod] = attributeLogic;
            //}

            //foreach (var testMethod in attributeLogicCache.Keys)
            //{
            //    var logic = attributeLogicCache[testMethod];
            //    logic.BeforeMethodExecution();

            //}
        }
    }
}
