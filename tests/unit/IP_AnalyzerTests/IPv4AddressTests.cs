using IpAnalyzer;
using Xunit;

namespace IpAnalyzer.Tests
{
    public class IPv4AddressTests
    {
        [Fact]
        public void Constructor_WithValidString_SetsAddressCorrectly()
        {
            var ip = new IPv4Address("192.168.1.10");
            Assert.Equal(new byte[] { 192, 168, 1, 10 }, ip.Address);
        }

        [Fact]
        public void Constructor_WithNullString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new IPv4Address((string)null!));
        }

        [Fact]
        public void Constructor_WithInvalidString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new IPv4Address("192.168.1"));
            Assert.Throws<ArgumentException>(() => new IPv4Address("192.168.1.256"));
            Assert.Throws<ArgumentException>(() => new IPv4Address("abc.def.ghi.jkl"));
        }

        [Fact]
        public void Constructor_WithValidByteArray_SetsAddressCorrectly()
        {
            var ip = new IPv4Address(new byte[] { 10, 0, 0, 1 });
            Assert.Equal(new byte[] { 10, 0, 0, 1 }, ip.Address);
        }

        [Fact]
        public void Constructor_WithNullByteArray_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new IPv4Address((byte[])null!));
        }

        [Fact]
        public void Constructor_WithInvalidByteArrayLength_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new IPv4Address([1, 2, 3]));
            Assert.Throws<ArgumentException>(() => new IPv4Address([1, 2, 3, 4, 5]));
        }

        [Fact]
        public void Parse_ValidString_ReturnsCorrectByteArray()
        {
            var result = IPv4Address.Parse("127.0.0.1");
            Assert.Equal(new byte[] { 127, 0, 0, 1 }, result);
        }

        [Fact]
        public void Parse_NullOrEmptyString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IPv4Address.Parse(null!));
            Assert.Throws<ArgumentNullException>(() => IPv4Address.Parse(""));
            Assert.Throws<ArgumentNullException>(() => IPv4Address.Parse("   "));
        }

        [Fact]
        public void Parse_InvalidFormat_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => IPv4Address.Parse("1.2.3"));
            Assert.Throws<ArgumentException>(() => IPv4Address.Parse("1.2.3.4.5"));
            Assert.Throws<ArgumentException>(() => IPv4Address.Parse("1.2.3.a"));
            Assert.Throws<ArgumentException>(() => IPv4Address.Parse("1.2.3.256"));
        }

        [Fact]
        public void ToString_ReturnsCorrectString()
        {
            var ip = new IPv4Address([8, 8, 8, 8]);
            Assert.Equal("8.8.8.8", ip.ToString());
        }
    }
}