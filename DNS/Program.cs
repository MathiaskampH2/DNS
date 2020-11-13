using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    class Program
    {
        
        static void Main(string[] args)
        {
            NetworkPing networkPing = new NetworkPing();
            DnsLookup dnsLookup = new DnsLookup();
            DhcpServer dhcpServer = new DhcpServer();
            UrlToIpAddress urlToIpAddress = new UrlToIpAddress();
            NetworkAdapterIpAddress networkAdapterIpAddress = new NetworkAdapterIpAddress();

            Console.Write("Insert a website URL :");

            string urlFromUser = Console.ReadLine();

            urlToIpAddress.TransformUrlToIp(urlFromUser);
            Console.WriteLine("local network information is :");
            Console.WriteLine(networkPing.LocalPing());

            Console.Write("insert DNS ip address :");
            string dnsFromUser = Console.ReadLine();
            string dnsHostName = dnsLookup.GetHostNameFromIp(dnsFromUser);
            Console.WriteLine("DNS name :" + dnsHostName);

            string hostNameIpFromHostname = dnsLookup.GetIpFromHostName(dnsHostName);
            Console.WriteLine("Waste Electrical & Electronic Equipment :" + hostNameIpFromHostname);

            string dnsTraceRoute = networkPing.TraceRoute(dnsFromUser);
            Console.WriteLine("route*** " + dnsTraceRoute);

            Console.WriteLine(dhcpServer.DisplayDHCPServerAddresses());

            Console.Write("Press any key to continue...");

            Console.ReadKey();

            Console.WriteLine();

            Console.Write("Insert your computer name to get adapter ipv4 / ipv6 addresses :");
            string hostName = Console.ReadLine(); // navnet på din pc

            Console.WriteLine(networkAdapterIpAddress.NetworkAdapterDetails(hostName));


            Console.ReadKey();
        }
    }

}