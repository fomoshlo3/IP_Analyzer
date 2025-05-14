using Dumpify;
using IP_Analyzer;
using IP_Analyzer.Services;
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
                JsonStorage.CreateDirectory();


                if(args.Length != 1)
                {
                    "Usage: IPAnalyzer <IP Address/CIDR Prefix>".DumpConsole();
                    return;
                }

                string[] input = args[0].Split('/');

                var analyzer = new Analyzer(input);
                var output = analyzer.CreateNetworkInfo();

                analyzer.NetworkInfo = output;

                JsonStorage.Save(analyzer.NetworkInfo);
                analyzer.NetworkInfo = JsonRetrieval.Load();

                analyzer.NetworkInfo.Dump();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".Dump();
            }
        }
    }
}
