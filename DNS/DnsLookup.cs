using System;
using System.Net;
using System.Net.Sockets;
using System.Security;

namespace DNS
{
    /// <summary>
    /// This class is responsible of looking up either an Ip address or URL.
    /// It has 2 methods.
    /// GetHostnameFromIp method gets an Ip address and returns a URL.
    /// GetIpFromHostname method gets an URL and returns a Ip address.
    /// </summary>
    public class DnsLookup
    {
        public string GetHostNameFromIp(string ip)
        {
            string hostName = "";
            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(ip);
                hostName = ipHostEntry.HostName;
            }
            catch (FormatException)
            {
                hostName = "Please specify a valid IP address.";
                return hostName;
            }
            catch (SocketException)
            {
                hostName = "Unable to perform lookup - a socket error occurred.";
                return hostName;
            }
            catch (SecurityException)
            {
                hostName = "Unable to perform lookup - permission denied.";
                return hostName;
            }
            catch (Exception)
            {
                hostName = "An unspecified error occurred.";
                return hostName;
            }

            return hostName;
        }

        public string GetIpFromHostName(string hostName)
        {
            string ip = "";
            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(hostName);
                if (ipHostEntry.AddressList.Length > 0)
                {
                    ip = ipHostEntry.AddressList[0].ToString();
                }
                else
                {
                    ip = "No information found.";
                }
            }
            catch (SocketException)
            {
                ip = "Unable to perform lookup - a socket error occurred.";
                return ip;
            }
            catch (SecurityException)
            {
                ip = "Unable to perform lookup - permission denied.";
                return ip;
            }
            catch (Exception)
            {
                ip = "An unspecified error occurred.";
                return ip;
            }

            return ip;
        }
    }
}