using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NextUnit.Core;
using NextUnit.Core.Extensions;
using NextUnitTestAdapter;
using System.Reflection;

namespace NextUnit.TestExplorer
{
	public class TestAdapterProvider
	{
		public ITestDiscoverer TestDiscoverer { get; private set; } = null;

		public ITestDiscoverer GetTestDiscoverer(string source)
		{
			ITestDiscoverer discoverer = null;
			try
			{
				discoverer = LoadAssembly(source);
			}
			catch (Exception ex)
			{
			}

			if (discoverer == null)
			{
				discoverer = new NextUnitTestDiscoverer();
			}
			return discoverer;
		}


		/// <summary>
		/// TODO: make the TestAdapter directory configurable
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		private ITestDiscoverer LoadAssembly(string source)
		{
			var assembly = Assembly.LoadFrom(source);

			ITestDiscoverer testDiscoverer = FindTestDiscovererInAssembly(assembly);
			if (testDiscoverer == null)
			{
				string testAdapterSource = @"..\TestAdapter\NUnit3.TestAdapter.dll";
				testDiscoverer = LoadTestAdapter(testAdapterSource);
			}
			if (testDiscoverer != null)
			{
				return testDiscoverer;				
			}
			return testDiscoverer;
		}

		private ITestDiscoverer FindTestDiscovererInAssembly(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				Type interfaceType = type.GetInterface<ITestDiscoverer>();
				if (interfaceType != null)
				{
					TestDiscoverer = Activator.CreateInstance(type) as ITestDiscoverer;
					return TestDiscoverer;
				}
			}
			return null;
		}

		/// <summary>
		/// TODO: make the TestAdapter directory configurable
		/// </summary>
		/// <param name="testAdapterSource"></param>
		/// <returns></returns>
		public ITestDiscoverer LoadTestAdapter(string testAdapterSource = @"..\TestAdapter\NUnit3.TestAdapter.dll")
		{
			var assembly = Assembly.LoadFrom(testAdapterSource);//Assembly.LoadFrom(testAdapterSource);
			ITestDiscoverer testDiscoverer = FindTestDiscovererInAssembly(assembly);
			return testDiscoverer;
		}

		public static Assembly? CurrentDomain_TypeResolve(object? sender, ResolveEventArgs args)
		{
			return null;
		}

		public static Assembly? CurrentDomain_ReflectionOnlyAssemblyResolve(object? sender, ResolveEventArgs args)
		{
			return null;
		}

		public static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
		{
		}

		public static void CurrentDomain_FirstChanceException(object? sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
		{
		}

		public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
		}

		public static void CurrentDomain_DomainUnload(object? sender, EventArgs e)
		{
		}

		public static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
		{
			return null;
		}

		public static void CurrentDomain_AssemblyLoad(object? sender, AssemblyLoadEventArgs args)
		{
		}
	}
}
