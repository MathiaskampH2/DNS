using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace DNS
{
    // this class is responsible of transforming a URL to An IpAddress
    // it has 1 method that takes a string (URL) as parameter
    public class UrlToIpAddress
    {
        public String TransformUrlToIp(string urlFromUser)
        {
            // create an array of ipAddresses based on that URL you typed
            IPAddress[] array = Dns.GetHostAddresses(urlFromUser);
            // empty string to store the IpAddress
            string ipAddress = null;
            // loop through the array
            foreach (IPAddress ip in array)
            {
                // store IpAddresses from the array in the string variable
               ipAddress = "The ip of the URl is : " + ip.ToString();
            }
            // return IpAddress
            return ipAddress;
        }
    }
}