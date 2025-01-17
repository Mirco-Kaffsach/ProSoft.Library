using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Moq;

namespace ProSoft.Library.UnitTest.Utils;

/// <summary>
/// Class MockBuilder.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MockBuilder
{
    /// <summary>
    /// Creates the logger mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logLevelEnabledResult">if set to <c>true</c> [log level enabled result].</param>
    /// <returns>Mock&lt;ILogger&lt;T&gt;&gt;.</returns>
    public static Mock<ILogger<T>> CreateLoggerMock<T>(bool logLevelEnabledResult)
    {
        var mockLogger = new Mock<ILogger<T>>();
        mockLogger
            .Setup(x => x.IsEnabled(It.IsAny<LogLevel>()))
            .Returns(logLevelEnabledResult);

        return mockLogger;
    }
}
