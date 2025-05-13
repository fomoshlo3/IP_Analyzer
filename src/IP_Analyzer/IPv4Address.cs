using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_Analyzer
{
    public sealed class IPv4Address
    {
        public byte[] Address { get; private set; }

        public IPv4Address(string ipString)
        {
            Address = Parse(ipString);
        }

        public IPv4Address(byte[] ipBytes)
        {
            if (ipBytes is null)
                throw new ArgumentException(nameof(ipBytes),"IPv4 address cannot be null.");
            if(ipBytes.Length != 4)
                throw new ArgumentException(nameof(ipBytes), "An IPv4 address must be 4 bytes long.");
            Address = ipBytes;
        }

        public static byte[] Parse(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
                throw new ArgumentNullException(nameof(ipString), "IPv4 address cannot be null or empty.");

            string[] octets = ipString.Split('.');
            if (octets.Length != 4)
                throw new ArgumentException("IPv4 address must consist of 4 octets.");

            byte[] result = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (!byte.TryParse(octets[i], out result[i]))
                    throw new ArgumentException("IPv4 address octets must be numbers between 0 and 255.");
            }

            return result;
        }

        public override string ToString()
        {
            return string.Join(".", Address);
        }
    }
}
