using Moq;
using System.Reflection;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// Caches the class instance objects to be used the method invocations.
    /// </summary>
    public class CacheInstanceObjects : IInstanceCreationBehavior
    {
        public Dictionary<Type, object> InstanceObjects = new Dictionary<Type, object>();
        
        public bool OnlyInitializeAtStartBehavior { get => true; }

        /// <summary>
        /// We use the testdiscoverer to detect the tests and
        /// then create the class instance objects as needed.
        /// </summary>
        /// <param name="testDiscoverer"></param>
        public void CacheObjects(ITestDiscoverer testDiscoverer)
        {
            var TestMethodsPerClass = testDiscoverer.Discover();
            //thus, we only need to create the instance objects per type here.
            foreach (var testDefinition in TestMethodsPerClass)
            {
                (Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes) definition = ((Type type, MethodInfo methodInfo, IEnumerable<Attribute> Attributes))testDefinition;
                Type definitionType = definition.type;
                if (!InstanceObjects.ContainsKey(definition.type))
                {
                    InstanceObjects.Add(definitionType, Activator.CreateInstance(definitionType));
                }
            }
        }

        /// <summary>
        /// Instanced of really creating it here, we return the
        /// object for this type if it already really exists.
        /// If it doesn't we throw an exception.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object CreateInstance(Type type)
        {
            if (InstanceObjects.ContainsKey(type))
            {
                return InstanceObjects[type];
            }
            else
            {
                object InstanceObject = Activator.CreateInstance(type);
                InstanceObjects.Add(type, InstanceObject);
                return InstanceObject;
            }
        }
    }
}
