using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_Analyzer
{
    public class Analyzer
    {
        internal IPv4Address GivenAddress { get; }
        internal IPv4Prefix GivenPrefix { get; }
        public NetworkInfo NetworkInfo { get; set; } = null!;

        public Analyzer(string[] input)
        {
            GivenAddress = new IPv4Address(input[0]);
            GivenPrefix = new IPv4Prefix(input[1]);
        }

        public NetworkInfo CreateNetworkInfo()
        {
            var subnetMask = NetworkCalculations.GetSubnetMask(GivenPrefix);
            var netId = NetworkCalculations.GetNetId(GivenAddress, subnetMask);
            var broadcast = NetworkCalculations.GetBroadcast(netId, subnetMask);
            var firstHost = NetworkCalculations.GetFirstHost(netId);
            var lastHost = NetworkCalculations.GetLastHost(broadcast);
            var totalHosts = NetworkCalculations.GetTotalUsableHosts(GivenPrefix);

            return new NetworkInfo(
                GivenAddress,
                subnetMask,
                broadcast,
                netId,
                firstHost,
                lastHost,
                totalHosts
            );
        }
    }
}
