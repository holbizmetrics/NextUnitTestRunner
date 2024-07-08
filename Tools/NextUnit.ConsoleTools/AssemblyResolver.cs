using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.ConsoleTools
{
	public class AssemblyResolver
	{
		static AssemblyResolver()
		{
			ActivateEventHandlers(true);
		}

		public static void ActivateEventHandlers(bool activate = true)
		{
			if (activate)
			{
				AppDomain.CurrentDomain.AssemblyLoad += AssemblyResolver.CurrentDomain_AssemblyLoad;
				AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver.CurrentDomain_AssemblyResolve;
				AppDomain.CurrentDomain.DomainUnload += AssemblyResolver.CurrentDomain_DomainUnload;
				AppDomain.CurrentDomain.UnhandledException += AssemblyResolver.CurrentDomain_UnhandledException;
				AppDomain.CurrentDomain.FirstChanceException += AssemblyResolver.CurrentDomain_FirstChanceException;
				AppDomain.CurrentDomain.ProcessExit += AssemblyResolver.CurrentDomain_ProcessExit;
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyResolver.CurrentDomain_ReflectionOnlyAssemblyResolve;
				AppDomain.CurrentDomain.TypeResolve += AssemblyResolver.CurrentDomain_TypeResolve;
			}
			else
			{
				AppDomain.CurrentDomain.AssemblyLoad -= AssemblyResolver.CurrentDomain_AssemblyLoad;
				AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolver.CurrentDomain_AssemblyResolve;
				AppDomain.CurrentDomain.DomainUnload -= AssemblyResolver.CurrentDomain_DomainUnload;
				AppDomain.CurrentDomain.UnhandledException -= AssemblyResolver.CurrentDomain_UnhandledException;
				AppDomain.CurrentDomain.FirstChanceException -= AssemblyResolver.CurrentDomain_FirstChanceException;
				AppDomain.CurrentDomain.ProcessExit -= AssemblyResolver.CurrentDomain_ProcessExit;
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AssemblyResolver.CurrentDomain_ReflectionOnlyAssemblyResolve;
				AppDomain.CurrentDomain.TypeResolve -= AssemblyResolver.CurrentDomain_TypeResolve;
			}
		}

		private static void InstantiateMyTypeSucceed(AppDomain domain)
		{
			try
			{
				string asmname = Assembly.GetCallingAssembly().FullName;
				domain.CreateInstance(asmname, "MyType");
			}
			catch (Exception e)
			{
				Console.WriteLine();
				Console.WriteLine(e.Message);
			}
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
			string locationDirectory = Path.GetDirectoryName(args.RequestingAssembly.Location);
			AssemblyName assemblyName = new AssemblyName(args.Name);
			string assemblyPath = Path.Combine(locationDirectory, assemblyName.Name + ".dll");

			Assembly? assembly = null;
			if (!loadedAssemblies.Contains(assemblyName.Name))
			{
				loadedAssemblies.Add(assemblyName.Name);

				try
				{
					assembly = Assembly.Load(assemblyName);
				}
				catch (Exception ex)
				{
					try
					{
						assembly = Assembly.LoadFrom(assemblyPath);
					}
					catch(Exception ex2)
					{

					}
				}
			}

			return assembly;
		}

		private static List<string> loadedAssemblies = new List<string>();

		public static void CurrentDomain_AssemblyLoad(object? sender, AssemblyLoadEventArgs args)
		{
		}

		public static Assembly? CurrentDomain_TypeResolve(object? sender, ResolveEventArgs args)
		{
			return null;
		}

		public static Assembly? CurrentDomain_ReflectionOnlyAssemblyResolve(object? sender, ResolveEventArgs args)
		{
			return null;
		}
	}
}