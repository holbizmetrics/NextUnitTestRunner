using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestRunner
{
    public class RecreateObjectInstanceForEachTest : IInstanceCreationBehavior
    {
        public bool OnlyInitializeAtStartBehavior => false;

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
