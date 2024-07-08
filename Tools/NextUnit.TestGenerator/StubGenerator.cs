using NextUnit.Core.Extensions;
using NextUnit.TestRunner;
using System.Reflection;

namespace NextUnit.TestGenerator
{
    /// <summary>
    /// Use this to generate Test Stubs.
    /// </summary>
    public class StubGenerator
    {
        /// <summary>
        /// When using false we can use this to create Tests from methods in classes that don't contain tests, yet.
        /// </summary>
        public bool CreateStubsFromTests { get; set; } = false;
        public IEnumerable<(Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes)> DiscoverTestsFromTypes(params Type[] types)
        {
            TestDiscoverer testDiscoverer = new TestDiscoverer();
            return testDiscoverer.Discover(types);
        }

		public OutputGenerator Create(params Type[] types)
		{
			OutputGenerator outputGenerator = new OutputGenerator();
			IEnumerable<(Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes)> definitions = CreateStubsFromTests ? DiscoverTestsFromTypes(types) : DiscoverMethodsOnly(types);
			foreach (var definition in definitions)
			{
				string testName = definition.methodInfo.Name;
				//string parameters = string.Join(", ", definition.methodInfo.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
				string parameters = string.Join(", ", definition.methodInfo.GetParameters().Select(x => x.ParameterType.FormatType()));
				string test = $@"
[Test]
public void {testName}Test({parameters})
{{
    //Arrange

    //Act

    //Assert
}}
";
				Type definitionType = definition.type;
				if (!outputGenerator.Output.ContainsKey(definitionType))
				{
					outputGenerator.Output.Add(definitionType, new List<string>() { test });
				}
				else
				{
					outputGenerator.Output[definitionType].Add(test);
				}
			}
			return outputGenerator;
		}

        public IEnumerable<(Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes)> DiscoverMethodsOnly(params Type[] types)
        {
            List<(Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes)> definitions = new List<(Type type, MethodInfo methodInfo, IEnumerable<Attribute> attributes)>();
            foreach (Type type in types)
            {
                MethodInfo[] methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (MethodInfo methodInfo2 in methodInfo)
                {
                    definitions.Add(new(type, methodInfo2, methodInfo2.GetCustomAttributes()));
                }
            }
            return definitions;
        }
    }
}
