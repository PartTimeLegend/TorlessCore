/*
  Copyright (c) 2021 Antony Bailey
  Distributed under the MIT software license, see the accompanying
  file LICENSE or http://www.opensource.org/licenses/mit-license.php.
*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
[assembly: System.Runtime.InteropServices.ComVisible(true)]
[assembly: CLSCompliant(true)]
namespace TorlessCore
{
    public static class TorlessCore
    {
        public static bool IsATorExitNode(string clientIpAddress, string serverIpAddress)
        {
            const int defaultPort = 80;
            return IsATorExitNode(clientIpAddress, serverIpAddress, defaultPort);
        }
        public static bool IsATorExitNode(string clientIpAddress, string serverIpAddress, int portToCheck)
        {
            string addressToCheck = ReverseGraphemeClusters(clientIpAddress) + "." + portToCheck + "." + ReverseGraphemeClusters(serverIpAddress) + ".ip-port.exitlist.torproject.org";
            IPHostEntry ipHostEntry = Dns.GetHostEntry(addressToCheck);

            return ipHostEntry.AddressList[0].ToString() == "127.0.0.2";
        }

        private static IEnumerable<string> GraphemeClusters(this string s)
        {
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(s);
            while (enumerator.MoveNext())
            {
                yield return (string)enumerator.Current;
            }
        }
        private static string ReverseGraphemeClusters(string s)
        {
            return string.Join("", s.GraphemeClusters().Reverse().ToArray());
        }

    }
}