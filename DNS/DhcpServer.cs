using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.NetworkInformation;

namespace DNS
{
    /// <summary>
    /// This class is responsible of getting all DHCP servers on the network
    /// Has a method called DisplayDHCPServerAddresses that returns a string with the DHCP server.
    /// </summary>
    public class DhcpServer
    {
        public string DisplayDHCPServerAddresses()
        {
            // empty string to store DHCP address
            string dhcpServerAddress = null;
            dhcpServerAddress = "DHCP Servers";
            // array of adapters
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            
            // loop through the array and get all adapters
            foreach (NetworkInterface adapter in adapters)
            {
                // get property of adapters
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                // get DHCP address of the adapters
                IPAddressCollection addresses = adapterProperties.DhcpServerAddresses;
                if (addresses.Count > 0)
                {
                    // print out the name of the Adapter
                    dhcpServerAddress = dhcpServerAddress + "\n" + adapter.Description;
                    foreach (IPAddress address in addresses)
                    {
                        // Loop through the DHCP addresses of the adapters
                        dhcpServerAddress = dhcpServerAddress + $"\nDHCP Address : {address.ToString()}";
                    }

                    Console.WriteLine();
                }
            }
            // return with adapter names, and their DHCP server ip address.
            return dhcpServerAddress;
        }
    }
}