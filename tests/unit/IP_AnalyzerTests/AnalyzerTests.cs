﻿using Xunit;
using IP_Analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_Analyzer.Tests
{
    public class AnalyzerTests
    {
        [Fact]
        public void CreateNetworkInfo_ReturnsExpectedNetworkInfo()
        {
            // Arrange
            string[] input = ["192.168.1.10", "24"];
            var analyzer = new Analyzer(input);

            // Act
            var networkInfo = analyzer.CreateNetworkInfo();

            // Assert
            Assert.Equal("192.168.1.10", networkInfo.Address.ToString());
            Assert.Equal("255.255.255.0", networkInfo.SubnetMask.ToString());
            Assert.Equal("192.168.1.255", networkInfo.Broadcast.ToString());
            Assert.Equal("192.168.1.0", networkInfo.NetworkId.ToString());
            Assert.Equal("192.168.1.1", networkInfo.FirstHost.ToString());
            Assert.Equal("192.168.1.254", networkInfo.LastHost.ToString());
            Assert.Equal(254, networkInfo.TotalHosts);
        }

        [Theory]
        [InlineData("10.0.0.1", "8", "255.0.0.0", "10.255.255.255", "10.0.0.0", "10.0.0.1", "10.255.255.254", 16777214)]
        [InlineData("172.16.5.4", "16", "255.255.0.0", "172.16.255.255", "172.16.0.0", "172.16.0.1", "172.16.255.254", 65534)]
        public void CreateNetworkInfo_VariousInputs_ReturnsExpectedResults(
            string ip, string prefix, string expectedMask, string expectedBroadcast, string expectedNetId,
            string expectedFirstHost, string expectedLastHost, int expectedTotalHosts)
        {
            // Arrange
            var input = new[] { ip, prefix };
            var analyzer = new Analyzer(input);

            // Act
            var networkInfo = analyzer.CreateNetworkInfo();

            // Assert
            Assert.Equal(ip, networkInfo.Address.ToString());
            Assert.Equal(expectedMask, networkInfo.SubnetMask.ToString());
            Assert.Equal(expectedBroadcast, networkInfo.Broadcast.ToString());
            Assert.Equal(expectedNetId, networkInfo.NetworkId.ToString());
            Assert.Equal(expectedFirstHost, networkInfo.FirstHost.ToString());
            Assert.Equal(expectedLastHost, networkInfo.LastHost.ToString());
            Assert.Equal(expectedTotalHosts, networkInfo.TotalHosts);
        }
    }
}