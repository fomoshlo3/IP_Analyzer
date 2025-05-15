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
        public Queue<NetworkInfo> History { get; set; } = new();

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

        public void AddToHistory(NetworkInfo networkInfo)
        {
            History.Enqueue(networkInfo);
            if (History.Count > 3)
            {
                History.Dequeue();
            }
        }

        public void AddRangeToHistory(IEnumerable<NetworkInfo> networkInfos)
        {
            foreach (var networkInfo in networkInfos)
            {
                AddToHistory(networkInfo);
            }
        }

    }
}
