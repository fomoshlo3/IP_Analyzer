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
                // Erstelle einen Ordner
                JsonStorage.CreateDirectory();

                // validate Input with Feedback
                if(args.Length != 1)
                {
                    "Usage: IPAnalyzer <IP Address/CIDR Prefix>".DumpConsole();
                    return;
                }

                string[] input = args[0].Split('/');

                // instantiate Factory class
                var analyzer = new Analyzer(input);

                // load history from file
                analyzer.AddRangeToHistory(JsonRetrieval.Load());

                // create NetworkInfo object
                var output = analyzer.CreateNetworkInfo();

                // add latest calculation to history
                analyzer.AddToHistory(output);

                // save history to file
                JsonStorage.Save(analyzer.History);

                // print the result to console
                foreach(var info in analyzer.History)
                {
                    info.Dump();
                }
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".Dump();
            }
        }
    }
}
