using NextUnit.TestRunner;
using System.Reflection;

namespace Blub.AdditionallyNeeded
{
    public class ExampleTestDiscoverer : TestDiscoverer
    {
        public override IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> Discover(params Type[] types)
        {
            return base.Discover(types);
        }

        public override Dictionary<string, (Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes, Delegate @delegate)> CreateTestDelegates(IEnumerable<(Type Type, MethodInfo Method, IEnumerable<Attribute> Attributes)> testMethodsPerClass, IInstanceCreationBehavior instanceCreationBehavior)
        {
            return base.CreateTestDelegates(testMethodsPerClass, instanceCreationBehavior);
        }
    }
}
