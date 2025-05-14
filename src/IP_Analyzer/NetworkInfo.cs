namespace IP_Analyzer;

public record NetworkInfo(
    IPv4Address Address,
    IPv4Address SubnetMask,
    IPv4Address Broadcast,
    IPv4Address NetworkId,
    IPv4Address FirstHost,
    IPv4Address LastHost,
    int TotalHosts);

public record NetworkInfoDTO(
   string Address,
   string SubnetMask,
   string Broadcast,
   string NetworkAddress,
   string FirstUsableAddress,
   string LastUsableAddress,
   int TotalUsableAddresses);

public static class InfoExtensions
{
    public static NetworkInfoDTO MapToDTO(this NetworkInfo toMap)
    {
        var (Address, SubnetMask, Broadcast, NetworkId, FirstHost, LastHost, TotalHosts) = toMap;

        return new NetworkInfoDTO(
            Address.ToString(),
            SubnetMask.ToString(),
            Broadcast.ToString(),
            NetworkId.ToString(),
            FirstHost.ToString(),
            LastHost.ToString(),
            TotalHosts
        );
    }

    public static NetworkInfo MapToNetworkInfo(this NetworkInfoDTO toMap)
    {
        var (Address, Subnetmask, Broadcast, NetworkId, FirstUsableAddress, LastUsableAddress, TotalUsableAddresses) = toMap;

        return new NetworkInfo(
            new IPv4Address(Address),
            new IPv4Address(Subnetmask),
            new IPv4Address(Broadcast),
            new IPv4Address(NetworkId),
            new IPv4Address(FirstUsableAddress),
            new IPv4Address(LastUsableAddress),
            TotalUsableAddresses
        );
    }
}
