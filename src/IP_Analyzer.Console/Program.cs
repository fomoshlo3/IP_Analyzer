using Dumpify;
using IP_Analyzer;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;

namespace IPAnalyzer.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if(args.Length != 1)
                {
                    "Usage: IPAnalyzer <IP Address/CIDR Prefix>".DumpConsole();
                    return;
                }

                string[] input = args[0].Split('/');

                var analyzer = new Analyzer(input);
                var output = analyzer.CreateNetworkInfo();

                output.Dump();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".Dump();
            }
        }
    }
}
