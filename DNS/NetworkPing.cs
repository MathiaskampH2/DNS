using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    /// <summary>
    /// This class is responsible of making you able to ping on the network
    /// it has 2 methods
    /// 1 method pings your local network adapter
    /// other method makes a traceRoute on a Ip address.
    /// </summary>
    public class NetworkPing
    {
        // prvate variable of ping that i can use in my 2 methods.
        private Ping pingSender = new Ping();

        public string LocalPing()
        {
           
            // create new instance of pingSender
            pingSender = new Ping();
            // sets ipAddress to loopback address (127.x.x.x)
            IPAddress address = IPAddress.Loopback;
            // stores replay of ping 
            PingReply reply = pingSender.Send(address);

            // check if replay = sucess
            if (reply.Status == IPStatus.Success)
            {
                // return ping information
                return
                    $"Address: {reply.Address.ToString()}\n"
                                   + $"RoundTrip time: {reply.RoundtripTime} \n"
                                   + $"RoundTrip time: {reply.RoundtripTime} \n"
                                   + $"Time to live: {reply.Options.Ttl} \n"
                                   + $"Don't fragment: {reply.Options.DontFragment} \n" 
                                   + $"Buffer size: {reply.Buffer.Length} \n";
            }
            else
            {
                // else return replay status
                return reply.Status.ToString();
            }
        }

        public string TraceRoute(string ipAddressToTrace)
        {
            // create new instance of IpAddress 
            IPAddress ipAddress = Dns.GetHostEntry(ipAddressToTrace).AddressList[0];
            // create a new instance of stringBuilder
            StringBuilder traceResults = new StringBuilder();

            // create new instance of pingSender
            using (pingSender = new Ping())
            {
                // create new instance of pingOptions
                PingOptions pingOptions = new PingOptions();
                // create new instance of stopWatch
                Stopwatch stopWatch = new Stopwatch();
                // create a new byte array of 32 bits (an ipAddress is 32 bits long)
                byte[] bytes = new byte[32];

                
                pingOptions.DontFragment = true;
                // set pingOptions to use TTl protocol
                pingOptions.Ttl = 1;
                // sets a variable to store max hops
                int maxHops = 30;

                // Adds ipAddress and maxHops to traceResults stringBuilder
                traceResults.AppendLine(
                    $"Tracing route to {ipAddress} over a maximum of {maxHops} hops:");

                traceResults.AppendLine();

                // loop to check on hops and get the ping replays.
                for (int i = 1; i < maxHops + 1; i++)
                {
                    stopWatch.Reset();
                    stopWatch.Start();
                    PingReply pingReply = pingSender.Send(
                        ipAddress,
                        5000,
                        new byte[32], pingOptions);

                    stopWatch.Stop();

                    if (pingReply != null)
                    {
                        traceResults.AppendLine(
                            $"{i}\t{stopWatch.ElapsedMilliseconds} ms\t{pingReply.Address}");

                        if (pingReply.Status == IPStatus.Success)
                        {
                            // add the pingReplay to traceResults stringBuilder
                            traceResults.AppendLine();
                            traceResults.AppendLine("Trace complete.");
                            break;
                        }
                    }

                    pingOptions.Ttl++;
                }
            }
            // return traceResult stringBuilder
            return traceResults.ToString();
        }
    }
}