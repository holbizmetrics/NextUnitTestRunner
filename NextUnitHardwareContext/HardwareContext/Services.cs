using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NextUnitHardwareContext.SystemInformation
{
    public class Services
    {
        public ServiceController[] GetServices()
        {
            return ServiceController.GetServices();
        }
    }
}
