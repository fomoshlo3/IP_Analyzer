using System.Net;

namespace IpAnalyzer
{
    internal static class NetworkCalculations
    {
        internal static IPv4Address GetSubnetMask(IPv4Prefix prefix)
        {
            // The subnet mask is a 32-bit number where the first 'prefix.Length' bits are 1s and the rest are 0s.
            uint mask = ~(0xff_ff_ff_ff >> prefix.Length);

            // Convert the mask to a byte array
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                // Shift the mask to the right by 24, 16, 8, and 0 bits respectively to get each byte
                bytes[i] = (byte)((mask >>  24 - (i * 8)) & 0xff);
            }

            return new IPv4Address(bytes);
        }

        internal static IPv4Address GetNetId(IPv4Address ipAddress, IPv4Address subnetMask)
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(ipAddress.Address[i] & subnetMask.Address[i]);
            }
            return new IPv4Address(bytes);
        }

        internal static IPv4Address GetBroadcast(IPv4Address networkAddress, IPv4Address subnetMask)
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(networkAddress.Address[i] | ~subnetMask.Address[i]);
            }
            return new IPv4Address(bytes);
        }

        internal static IPv4Address GetFirstHost(IPv4Address networkAddress)
        {
            var firstUsableAddress = new byte[4];
            
            Array.Copy(networkAddress.Address, firstUsableAddress, 4);

            firstUsableAddress[3] += 1; // Increment the last byte to get the first usable address
            return new IPv4Address(firstUsableAddress);
        }

        internal static IPv4Address GetLastHost(IPv4Address broadcastAddress)
        {
            var lastUsableAddress = new byte[4];

            Array.Copy(broadcastAddress.Address, lastUsableAddress, 4);
            lastUsableAddress[3] -= 1; // Decrement the last byte to get the last usable address

            return new IPv4Address(lastUsableAddress);
        }

        internal static int GetTotalUsableHosts(IPv4Prefix prefix)
        {
            return (int)Math.Pow(2, 32 - prefix.Length) - 2;
        }
    }
}
