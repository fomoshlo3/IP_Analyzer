namespace IP_Analyzer
{
    public sealed class IPv4Prefix
    {
        public int Length { get; private set; }

        public IPv4Prefix(string prefix)
        {
            Length = Parse(prefix);
        }

        public static int Parse(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix), "Prefix cannot be null or empty.");

            if (!int.TryParse(prefix, out int length) || length < 1 || length > 31)
                throw new ArgumentOutOfRangeException(nameof(prefix), "Prefix must be a number between 1 and 31.");

            return length;
        }
    }
}
