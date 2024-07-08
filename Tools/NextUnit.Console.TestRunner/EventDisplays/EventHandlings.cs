using NextUnit.TestRunner.TestRunners.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Console.TestRunner.EventDisplays
{
	public class EventHandlings
	{
		protected ITestRunner5 _testRunner5 = null;
		protected bool disposedValue;

		public virtual void Initialize(ITestRunner5 testRunner5)
		{
			_testRunner5 = testRunner5;
		}
		public virtual void Deinitialize()
		{
		}
		public virtual string Name { get; private set; }
	}
}
