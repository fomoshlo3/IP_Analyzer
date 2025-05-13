using IP_Analyzer;
using Xunit;

namespace IP_Analyzer.Tests
{
    public class IPv4PrefixTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("8", 8)]
        [InlineData("16", 16)]
        [InlineData("24", 24)]
        [InlineData("31", 31)]
        public void Constructor_ValidPrefix_SetsLength(string prefix, int expectedLength)
        {
            var ipv4Prefix = new IPv4Prefix(prefix);
            Assert.Equal(expectedLength, ipv4Prefix.Length);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("31", 31)]
        public void Parse_ValidPrefix_ReturnsLength(string prefix, int expectedLength)
        {
            int length = IPv4Prefix.Parse(prefix);
            Assert.Equal(expectedLength, length);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Parse_NullOrEmpty_ThrowsArgumentNullException(string prefix)
        {
            Assert.Throws<ArgumentNullException>(() => IPv4Prefix.Parse(prefix));
        }

        [Theory]
        [InlineData("0")]
        [InlineData("32")]
        [InlineData("-1")]
        [InlineData("abc")]
        [InlineData("1.1")]
        public void Parse_InvalidOrOutOfRange_ThrowsArgumentOutOfRangeException(string prefix)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => IPv4Prefix.Parse(prefix));
        }
    }
}
