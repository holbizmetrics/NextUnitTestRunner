using System.Diagnostics;
using System.Reflection;

namespace NextUnit.Benchmarking
{
    public static class BenchmarkHelper
    {
        public static bool IsDebugMode
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var attributes = assembly.GetCustomAttributes(typeof(DebuggableAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    var debuggableAttribute = attributes[0] as DebuggableAttribute;
                    if (debuggableAttribute != null)
                    {
                        if (debuggableAttribute != null)
                        {
                            return debuggableAttribute.IsJITOptimizerDisabled;
                        }
                    }
                }
                return false;
            }
        }
    }
}
