using Microsoft.Diagnostics.NETCore.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.TestRunner.NewFolder
{
    public class Helpers
    {
        /// <summary>
        /// Use this to create a dump of the given processId.
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="dumpType"></param>
        public static void TriggerCoreDump(int processId, DumpType dumpType = DumpType.Normal)
        {
            var client = new DiagnosticsClient(processId);
            client.WriteDump(DumpType.Normal, $"/tmp/minidump_{processId}_{Process.GetProcessById(processId).ProcessName}.dmp");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processIDs"></param>
        public static void TriggerCoreDumps(params int[] processIDs)
        {
            foreach (int processId in processIDs)
            {
                TriggerCoreDump(processId);
            }
        }
    }
}
