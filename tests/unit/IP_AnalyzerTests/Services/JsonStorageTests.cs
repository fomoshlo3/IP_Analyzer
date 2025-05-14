using System;
using System.IO;
using Xunit;

namespace IP_AnalyzerTests.Services
{
    public class JsonStorageTests
    {
        [Fact]
        public void CreateDirectory_ShouldCreateDirectory_WhenItDoesNotExist()
        {
            // Arrange
            string testPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestAppData");
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }

            // Act
            typeof(IP_Analyzer.Services.JsonStorage)
                .GetField("AppDataPath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                .SetValue(null, testPath);

            IP_Analyzer.Services.JsonStorage.CreateDirectory();

            // Assert
            Assert.True(Directory.Exists(testPath));

            // Cleanup
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }

        [Fact]
        public void CreateDirectory_ShouldNotThrow_WhenDirectoryAlreadyExists()
        {
            // Arrange
            string testPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestAppData");
            Directory.CreateDirectory(testPath);

            typeof(IP_Analyzer.Services.JsonStorage)
                .GetField("AppDataPath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                .SetValue(null, testPath);

            // Act & Assert
            var exception = Record.Exception(() => IP_Analyzer.Services.JsonStorage.CreateDirectory());
            Assert.Null(exception);

            // Cleanup
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }
    }
}