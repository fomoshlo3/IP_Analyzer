using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IpAnalyzer
{
    public record NetworkInfo
    {
        public NetworkInfo(IPv4Address address, IPv4Address subnetMask, IPv4Address broadcastAddress, IPv4Address networkAddress, IPv4Address firstUsableAddress, IPv4Address lastUsableAddress, int totalUsableAddresses)
        {
            Address = address;
            SubnetMask = subnetMask;
            BroadcastAddress = broadcastAddress;
            NetworkAddress = networkAddress;
            FirstUsableAddress = firstUsableAddress;
            LastUsableAddress = lastUsableAddress;
            TotalUsableAddresses = totalUsableAddresses;
        }

        public IPv4Address Address { get; init; }
        public IPv4Address SubnetMask { get; init; }
        public IPv4Address BroadcastAddress { get; init; }
        public IPv4Address NetworkAddress { get; init; }
        public IPv4Address FirstUsableAddress { get; init; }
        public IPv4Address LastUsableAddress { get; init; }
        public int TotalUsableAddresses { get; init; }
    }
}
