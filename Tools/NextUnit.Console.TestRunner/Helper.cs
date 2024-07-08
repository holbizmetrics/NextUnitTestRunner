namespace NextUnit.Console.TestRunner
{
	public static class Helper
	{
		private static Lazy<IEnumerable<string>> _testDLLs = null;
		public static IEnumerable<string> TestDLLs
		{
			get
			{
				_testDLLs = new Lazy<IEnumerable<string>>(ReadTestDLLs());
				return _testDLLs.Value;
			}
		}

		private static IEnumerable<string> ReadTestDLLs()
		{
			string[] assemblyPaths = NextUnit.Core.Extensions.ReflectionExtensions.GetAllAssembliesFromSolutionTopLevelDirectory(@"..\..\..\");
			IEnumerable<string> testDLLs = assemblyPaths.Where(x => x.Contains("NextUnit.") && x.EndsWith(".Tests.dll") && !x.Contains(@"obj\"));
			return testDLLs;
		}
	}
}
