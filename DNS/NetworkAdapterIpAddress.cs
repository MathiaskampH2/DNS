using System;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace DNS
{
    /// <summary>
    /// This class is responsible of getting Ip address information of the network adapter
    /// method NetworkAdapterDetails gets a hostName (computer name) and returns the information of the
    /// network adapters attached to the computer
    /// </summary>
    public class NetworkAdapterIpAddress
    {
        public string NetworkAdapterDetails(string hostName)
        {
            // string variable to save method parameter in a new variable
            string pcHostName = hostName;
            string hostPcDetails = null;
            
            IPHostEntry hostInfo = Dns.GetHostEntry(pcHostName);

            // Get the IP address list that resolves to the host names contained in the 
            // Alias property.
            IPAddress[] address = hostInfo.AddressList;

            // Get the alias names of the addresses in the IP address list.
            String[] alias = hostInfo.Aliases;

            // saves hostName information in hostPcDetails variable
            hostPcDetails = $"\nHost name : {hostInfo.HostName} \nAliases : ";

            // loop through alias array
            for (int index = 0; index < alias.Length; index++)
            {
                // save information of alias in hostPcDetails variable
                hostPcDetails = hostPcDetails + "\n" + alias[index].ToString();
            }
            // extends the hostPcDetails variable with more text
            hostPcDetails = hostPcDetails + "\nIP address list : ";

            // loop through address array
            for (int index = 0; index < address.Length; index++)
            {
                // extends hostPcDetails variable with addresses.
                hostPcDetails = hostPcDetails + "\n" + address[index].ToString();
            }
            // return hostPcDetail variable.
            return hostPcDetails;

        }

    }
}