using System;
using System.IO;
using NUnit.Framework;

public class Logger
{
    public static void LogMessage(string filename, string message, string level)
    {
        string timestamp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]");
        string logEntry = $"{timestamp} [{level}] {message}\n";
        File.AppendAllText(filename, logEntry);
    }
}

// NUnit Test Cases

[TestFixture]
public class LoggerTests
{
    [SetUp]
    public void Setup()
    {
        // Clean up existing log file
        File.WriteAllText("test.log", string.Empty);
    }

    [Test]
    public void LogMessage_WritesLogEntryToFile()
    {
        // Arrange
        string filename = "test.log";
        string message = "User logged in";
        string level = "INFO";

        // Act
        Logger.LogMessage(filename, message, level);

        // Assert
        string[] logContent = File.ReadAllLines(filename);
        Assert.Equals(1, logContent.Length);
        Assert.That(logContent[0].StartsWith("["));
        Assert.That(logContent[0], Does.Contain("[INFO] User logged in"));
    }

    [Test]
    public void LogMessage_WritesMultipleLogEntriesToFile()
    {
        // Arrange
        string filename = "test.log";
        
        // Act
        Logger.LogMessage(filename, "User logged in", "INFO");
        Logger.LogMessage(filename, "Failed login attempt", "WARNING");

        // Assert
        string[] logContent = File.ReadAllLines(filename);
        Assert.Equals(2, logContent.Length);
        Assert.That(logContent[0].Contains("[INFO] User logged in"));
        Assert.That(logContent[1].Contains("[WARNING] Failed login attempt"));
    }
}
