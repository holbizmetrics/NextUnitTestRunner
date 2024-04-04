using System.ServiceProcess;

namespace NextUnit.HardwareContext.SystemInformation
{
    public class Services
    {
        public ServiceController[] GetServices()
        {
            return ServiceController.GetServices();
        }
    }
}
