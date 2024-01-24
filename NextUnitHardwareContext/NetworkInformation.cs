using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.HardwareContext
{
    public class NetworkInfo
    {
        public static List<string> NetWorkInterfaces()
        {
            List<string> netWorkInterfaces = new List<string>();
            NetworkInterface[] netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            if (netInterfaces.Length < 1 || netInterfaces == null)
            {
                netWorkInterfaces.Add("No Network card installed on your machine...");
                return netWorkInterfaces;
            }

            //retrieves all network interface cards
            netWorkInterfaces.Add($"Total Network card installed on your machine is: {netInterfaces.Length}");

            //loop all the NICs
            foreach (NetworkInterface networkInterface in netInterfaces)
            {
                //retrieves ip related information from NIC
                IPInterfaceProperties ipProp = networkInterface.GetIPProperties();
                string info =
$@"{Environment.NewLine}{new string('-', 30)}{Environment.NewLine}
NIC ID: {networkInterface.Id}
Description: {networkInterface.Description}
Name: {networkInterface.Name}
    Physical Address: {networkInterface.GetPhysicalAddress().ToString()}
    Interface Type: {networkInterface.NetworkInterfaceType}
    Operational Status: {networkInterface.OperationalStatus}
    Supports Multicast: {networkInterface.SupportsMulticast}

    IPv4: {(networkInterface.Supports(NetworkInterfaceComponent.IPv4) ? "Yes" : "No")}
    IPv6: {(networkInterface.Supports(NetworkInterfaceComponent.IPv6) ? "Yes" : "No")}

    IP: {networkInterface.GetIPProperties()}
    DNS Enabled: {ipProp.IsDnsEnabled}
    DNS Suffix: {ipProp.DnsSuffix}
    Dynamically enabled DNS: {ipProp.IsDynamicDnsEnabled}
";

                if (networkInterface.Supports(NetworkInterfaceComponent.IPv4))
                {
                    info += ShowIPAddresses(ipProp);
                }
                netWorkInterfaces.Add(info);
            }
            return netWorkInterfaces;
        }

        public static string ShowIPAddresses(IPInterfaceProperties properties)
        {
            string ipAddresses = null;

            IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
            ipAddresses += $"   MTU...................................... : {ipv4.Mtu}" + Environment.NewLine;
            if (ipv4.UsesWins)
            {

                IPAddressCollection winsServers = properties.WinsServersAddresses;
                if (winsServers.Count > 0)
                {
                    ipAddresses += "    WINS Servers ............................ :";
                    winsServers = properties.WinsServersAddresses;
                    foreach (IPAddress ipAddress in winsServers)
                    {
                        ipAddresses += ipAddress.ToString() + Environment.NewLine;
                    }
                }
            }
            return ipAddresses;
        }
    }
}
